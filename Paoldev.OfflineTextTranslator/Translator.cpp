#include "pch.h"
#include "Translator.h"
#include "Translator.g.cpp"

#pragma warning(push)
#pragma warning(disable: 4244)  //v3.17.1: warning C4244: 'argument': conversion from 'const ctranslate2::dim_t' to '_Ty', possible loss of data
#include "ctranslate2/translator.h"
#pragma warning(pop)

#pragma warning(push)
#pragma warning(disable: 4267)  //v0.97: warning C4267 : '=' : conversion from 'size_t' to 'unsigned int', possible loss of data
#include "onmt/Tokenizer.h"
#include "onmt/BPE.h"
#include "onmt/SentencePiece.h"
#pragma warning(pop)

#include <pplawait.h>
#include <regex>

namespace winrt::Paoldev::OfflineTextTranslator::implementation
{
	static Translator::stdstring_vector to_stdstring_vector(const Translator::hstring_ivector& values)
	{
		Translator::stdstring_vector output;
		output.reserve(values.Size());
		std::for_each(values.begin(), values.end(), [&output](const hstring& text) {output.push_back(to_string(text)); });
		return output;
	}

	winrt::Windows::Foundation::IAsyncOperation<winrt::Paoldev::OfflineTextTranslator::ITranslator> Translator::CreateAsync(const TranslatorOptions& options)
	{
		ITranslator result = co_await concurrency::create_task([_options = options]()
			{
				auto translator = make<Paoldev::OfflineTextTranslator::implementation::Translator>(_options);
				return translator.as<ITranslator>();
			});
		co_return result;
	}

	Translator::Translator(const TranslatorOptions& options)
	{
		onmt::Tokenizer::Options tokenizerOptions;
		tokenizerOptions.joiner = onmt::Tokenizer::joiner_marker;

		std::shared_ptr<const onmt::SubwordEncoder> subword_encoder;

		if (options.TokenizerModelFilePath.size() > 0)
		{
			switch (options.TokenizerModelType)
			{
			case TokenizerType::SentencePiece:
			{
				int nbest_size = 0;
				float alpha = 1.0f;
				tokenizerOptions.mode = onmt::Tokenizer::Mode::None;
				subword_encoder = std::make_shared<const onmt::SentencePiece>(to_string(options.TokenizerModelFilePath), nbest_size, alpha);
				break;
			}

			case TokenizerType::BPE:
			{
				float dropout = 0.0f;
				tokenizerOptions.mode = onmt::Tokenizer::Mode::None;
				subword_encoder = std::make_shared<const onmt::BPE>(to_string(options.TokenizerModelFilePath), tokenizerOptions.joiner, dropout);
				break;
			}
			}
		}

		m_tokenizer = std::make_unique<onmt::Tokenizer>(tokenizerOptions, subword_encoder);

		ctranslate2::Device device{};
		switch (options.Device)
		{
		case DeviceType::Cpu:
			device = ctranslate2::Device::CPU;
			break;

		default:
			// cuda will be automatically selected if it's available.
			device = ctranslate2::str_to_device("auto");
			break;
		}
		m_translator = std::make_unique<ctranslate2::Translator>(to_string(options.CTranslate2ModelDirectoryPath), device);
	}

	/// <summary>
	/// Translate a string from source language to destination language.
	/// InSourceLanguage and InDestLanguage MUST be already formatted with model-specific prefixes; they may be empty.
	/// Exceptions may be thrown.
	/// </summary>
	/// <param name="InSourceText">the source string to be translated.</param>
	/// <param name="InSourceLanguage">the source string language, already formatted with model-specific prefix; it may be empty.</param>
	/// <param name="InDestLanguage">the desired translation language, already formatted with model-specific prefix; it may be empty.</param>
	/// <returns>The translated string.</returns>
	winrt::Windows::Foundation::IAsyncOperation<winrt::hstring> Translator::TranslateAsync(const hstring& InSourceText, const hstring& InSourceLanguage, const hstring& InDestLanguage)
	{
		std::string text(to_string(InSourceText));
		std::string sourceLanguage(to_string(InSourceLanguage));
		std::string destLanguage(to_string(InDestLanguage));

		auto strong_this{ get_strong() };

		hstring_ivector result = co_await concurrency::create_task([_text = std::move(text), _sourceLanguage = std::move(sourceLanguage), _destLanguage = std::move(destLanguage), strong_this]()
			{
				std::vector<hstring> TranslatedBatches;
				strong_this->TranslateInternal({ _text }, { _sourceLanguage }, { _destLanguage }, TranslatedBatches);
				return winrt::single_threaded_vector<hstring>(std::move(TranslatedBatches));
			});
		co_return result.GetAt(0);
	}

	/// <summary>
	/// Translate many strings from source language to destination language.
	/// InSourceLanguage and InDestLanguage MUST be already formatted with model-specific prefixes; they may be empty.
	/// Exceptions may be thrown.
	/// </summary>
	/// <param name="InSourceTexts">the source strings to be translated.</param>
	/// <param name="InSourceLanguage">the source strings language, already formatted with model-specific prefix; it may be empty.</param>
	/// <param name="InDestLanguage">the desired translation language, already formatted with model-specific prefix; it may be empty.</param>
	/// <returns>The translated strings; the output array size matches InSourceTexts size.</returns>
	winrt::Windows::Foundation::IAsyncOperation<Translator::hstring_ivector> Translator::TranslateAsync(const hstring_ivector& InSourceTexts, const hstring& InSourceLanguage, const hstring& InDestLanguage)
	{
		stdstring_vector batches(to_stdstring_vector(InSourceTexts));
		std::string sourceLanguage(to_string(InSourceLanguage));
		std::string destLanguage(to_string(InDestLanguage));

		auto strong_this{ get_strong() };

		hstring_ivector result = co_await concurrency::create_task([_batches = std::move(batches), _sourceLanguage = std::move(sourceLanguage), _destLanguage = std::move(destLanguage), strong_this]()
			{
				std::vector<hstring> TranslatedBatches;
				strong_this->TranslateInternal(_batches, stdstring_vector(_batches.size(), _sourceLanguage), stdstring_vector(_batches.size(), _destLanguage), TranslatedBatches);
				return winrt::single_threaded_vector<hstring>(std::move(TranslatedBatches));
			});
		co_return result;
	}

	/// <summary>
	/// Translate a string from source language to many destination languages.
	/// InSourceLanguage and InDestLanguages MUST be already formatted with model-specific prefixes; they may be empty.
	/// Exceptions may be thrown.
	/// </summary>
	/// <param name="InSourceText">the source string to be translated.</param>
	/// <param name="InSourceLanguage">the source string language, already formatted with model-specific prefix; it may be empty.</param>
	/// <param name="InDestLanguages">the desired translation languages, already formatted with model-specific prefix; it may be an empty array or some of its elements may be empty.</param>
	/// <returns>The string translated to many languages; the output array size matches InDestLanguages size.</returns>
	winrt::Windows::Foundation::IAsyncOperation<Translator::hstring_ivector>
		Translator::TranslateAsync(const hstring& InSourceText, const hstring& InSourceLanguage, const hstring_ivector& InDestLanguages)
	{
		stdstring_vector batches(InDestLanguages.Size(), to_string(InSourceText));  //TODO: optimize this part; InSourceText could be shared by all translators, one per destination language.
		std::string sourceLanguage = to_string(InSourceLanguage);
		stdstring_vector destLanguages(to_stdstring_vector(InDestLanguages));

		auto strong_this{ get_strong() };

		hstring_ivector result = co_await concurrency::create_task([_batches = std::move(batches), _sourceLanguage = std::move(sourceLanguage), _destLanguages = std::move(destLanguages), strong_this]()
			{
				std::vector<hstring> TranslatedBatches;
				strong_this->TranslateInternal(_batches, stdstring_vector(_batches.size(), _sourceLanguage), _destLanguages, TranslatedBatches);
				return winrt::single_threaded_vector<hstring>(std::move(TranslatedBatches));
			});
		co_return result;
	}

	/// <summary>
	/// Translate many strings, each one with a specific source language, to the same destination language.
	/// InSourceLanguages and InDestLanguage MUST be already formatted with model-specific prefixes; they may be empty.
	/// InSourceLanguages array can be empty; otherwise its size MUST match InSourceTexts size.
	/// Exceptions may be thrown.
	/// </summary>
	/// <param name="InSourceTexts">the source strings to be translated.</param>
	/// <param name="InSourceLanguages">the source strings language, already formatted with model-specific prefix; it may be empty, otherwise its size MUST match InSourceTexts size.</param>
	/// <param name="InDestLanguage">the desired translation language, already formatted with model-specific prefix; it may be empty.</param>
	/// <returns>The strings all translated to the same language; the output array size matches InSourceTexts size.</returns>
	winrt::Windows::Foundation::IAsyncOperation<Translator::hstring_ivector>
		Translator::TranslateAsync(const hstring_ivector& InSourceTexts, const hstring_ivector& InSourceLanguages, const hstring& InDestLanguage)
	{
		stdstring_vector batches(to_stdstring_vector(InSourceTexts));
		stdstring_vector sourceLanguages(to_stdstring_vector(InSourceLanguages));
		std::string destLanguage(to_string(InDestLanguage));

		auto strong_this{ get_strong() };

		hstring_ivector result = co_await concurrency::create_task([_batches = std::move(batches), _sourceLanguages = std::move(sourceLanguages), _destLanguage = std::move(destLanguage), strong_this]()
			{
				std::vector<hstring> TranslatedBatches;
				strong_this->TranslateInternal(_batches, _sourceLanguages, stdstring_vector(_batches.size(), _destLanguage), TranslatedBatches);
				return winrt::single_threaded_vector<hstring>(std::move(TranslatedBatches));
			});
		co_return result;
	}

	/// <summary>
	/// Translate many strings, each one with a specific source language, to a specific destination language each one.
	/// InSourceLanguages and InDestLanguages MUST be already formatted with model-specific prefixes; they may be empty.
	/// InSourceLanguages and/or InDestLanguages array can be empty; otherwise their size MUST match InSourceTexts size.
	/// Exceptions may be thrown.
	/// </summary>
	/// <param name="InSourceTexts">the source strings to be translated.</param>
	/// <param name="InSourceLanguages">the source strings language, already formatted with model-specific prefix; it may be an empty array, otherwise its size MUST match InSourceTexts size and some of its elements may be empty.</param>
	/// <param name="InDestLanguages">the desired translation languages, already formatted with model-specific prefix; it may be an empty array, otherwise its size MUST match InSourceTexts size and some of its elements may be empty.</param>
	/// <returns>The strings translated to their specific destination language; the output array size matches InSourceTexts size.</returns>
	winrt::Windows::Foundation::IAsyncOperation<Translator::hstring_ivector>
		Translator::TranslateAsync(const hstring_ivector& InSourceTexts, const hstring_ivector& InSourceLanguages, const hstring_ivector& InDestLanguages)
	{
		stdstring_vector batches(to_stdstring_vector(InSourceTexts));
		stdstring_vector sourceLanguages(to_stdstring_vector(InSourceLanguages));
		stdstring_vector destLanguages(to_stdstring_vector(InDestLanguages));

		auto strong_this{ get_strong() };

		hstring_ivector result = co_await concurrency::create_task([_batches = std::move(batches), _sourceLanguages = std::move(sourceLanguages), _destLanguages = std::move(destLanguages), strong_this]()
			{
				std::vector<hstring> TranslatedBatches;
				strong_this->TranslateInternal(_batches, _sourceLanguages, _destLanguages, TranslatedBatches);
				return winrt::single_threaded_vector<hstring>(std::move(TranslatedBatches));
			});
		co_return result;
	}

	/// <summary>
	/// Translate many strings, each one with a specific source language, to all specified destination languages.
	/// InSourceLanguages and InDestLanguages MUST be already formatted with model-specific prefixes; they may be empty.
	/// InSourceLanguages array can be empty; otherwise its size MUST match InSourceTexts size.
	/// InDestLanguages array can be empty or it can be of any size.
	/// Exceptions may be thrown.
	/// </summary>
	/// <param name="InSourceTexts">the source strings to be translated.</param>
	/// <param name="InSourceLanguages">the source strings language, already formatted with model-specific prefix; it may be empty, otherwise its size MUST match InSourceTexts size.</param>
	/// <param name="InDestLanguages">the desired translation languages for each source string.</param>
	/// <returns>The strings each one translated to all specified destination languages; the output array size is the product of InSourceTexts size and InDestLanguages size. To access the N-th translation of the M-th source string, the required index is "M * InSourceTexts.size() + N" (where M and N are zero-based indices).
	/// If InDestLanguages is empty, the output array size matches InSourceTexts size and each source string has just one translation (as if InDestLanguages was an array of 1 element containing an empty string as destination language).</returns>
	winrt::Windows::Foundation::IAsyncOperation<Translator::hstring_ivector>
		Translator::MultiTranslateAsync(const hstring_ivector& InSourceTexts, const hstring_ivector& InSourceLanguages, const hstring_ivector& InDestLanguages)
	{
		stdstring_vector batches(to_stdstring_vector(InSourceTexts));
		stdstring_vector sourceLanguages(to_stdstring_vector(InSourceLanguages));
		stdstring_vector destLanguages(to_stdstring_vector(InDestLanguages));

		auto strong_this{ get_strong() };

		hstring_ivector result = co_await concurrency::create_task([_batches = std::move(batches), _sourceLanguages = std::move(sourceLanguages), _destLanguages = std::move(destLanguages), strong_this]()
			{
				std::vector<hstring> TranslatedBatches;
				strong_this->TranslateInternal(_batches, _sourceLanguages, _destLanguages, TranslatedBatches, true);
				return winrt::single_threaded_vector<hstring>(std::move(TranslatedBatches));
			});
		co_return result;
	}

	void Translator::TranslateInternal(const stdstring_vector& InSourceTexts, const stdstring_vector& InSourceLanguages, const stdstring_vector& InDestLanguages, std::vector<hstring>& OutTranslatedBatches, const bool InMultiTranslate) const
	{
		if ((InSourceLanguages.size() && (InSourceLanguages.size() != InSourceTexts.size())) || (!InMultiTranslate && InDestLanguages.size() && (InDestLanguages.size() != InSourceTexts.size())))
		{
			throw hresult_invalid_argument(to_hstring("One input stream has less examples than the others"));
		}

		// detect if "language" prefix is needed
		auto has_prefix = [](const stdstring_vector& InLanguageContainer, const size_t InLanguageIndex)
			{
				return ((InLanguageIndex < InLanguageContainer.size()) && InLanguageContainer[InLanguageIndex].size());
			};

		// build "language" prefix string
		auto to_prefix = [&](const stdstring_vector& InLanguageContainer, const size_t InLanguageIndex)
			{
				return has_prefix(InLanguageContainer, InLanguageIndex) ? InLanguageContainer[InLanguageIndex] : "";
			};

		std::vector<stdstring_vector> tokens_batches;
		std::vector<stdstring_vector> target_prefix;

		const size_t num_texts = InSourceTexts.size();
		const size_t num_translations_per_text = (!InMultiTranslate) ? 1 : std::max(1LLU, InDestLanguages.size());
		const size_t num_batches = num_texts * num_translations_per_text;
		tokens_batches.reserve(num_batches);
		target_prefix.reserve(num_batches);

		for (uint32_t i = 0; i < num_texts; i++)
		{
			std::vector<std::string> tokens;
			m_tokenizer->tokenize(InSourceTexts[i], tokens);

			//Insert source prefix only if it's a valid string.
			if (has_prefix(InSourceLanguages, i))
			{
				tokens.insert(tokens.begin(), to_prefix(InSourceLanguages, i));
			}

			for (uint32_t j = 0; j < num_translations_per_text; j++)
			{
				tokens_batches.push_back(tokens);

				//Always insert a destination prefix; it can be an empty string.
				const size_t destLanguageIndex = (!InMultiTranslate) ? i : j;
				target_prefix.push_back({ to_prefix(InDestLanguages, destLanguageIndex) });
			}
		}

		ctranslate2::TranslationOptions options;
		options.max_input_length = 0;
		const std::vector<ctranslate2::TranslationResult> results =
			m_translator->translate_batch(tokens_batches, target_prefix, options);

		OutTranslatedBatches.clear();
		OutTranslatedBatches.reserve(results.size());
		for (uint32_t i = 0; i < results.size(); i++)
		{
			const stdstring_vector& translatedTokens = results[i].output();

			const size_t destLanguageIndex = (!InMultiTranslate) ? i : (i % num_translations_per_text);

			std::string result;
			if (has_prefix(InDestLanguages, destLanguageIndex))
			{
				result = m_tokenizer->detokenize(stdstring_vector(translatedTokens.begin()+1, translatedTokens.end()));
			}
			else
			{
				result = m_tokenizer->detokenize(translatedTokens);
			}

			OutTranslatedBatches.push_back(to_hstring(result));
		}
	}
}
