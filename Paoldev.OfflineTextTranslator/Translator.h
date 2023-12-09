#pragma once

#include "Translator.g.h"

namespace onmt
{
    class Tokenizer;
}

namespace ctranslate2
{
    class Translator;
}

namespace winrt::Paoldev::OfflineTextTranslator::implementation
{
    /// <summary>
    /// The Translator class
    /// </summary>
    struct Translator : TranslatorT<Translator>
    {
        using hstring_ivector = winrt::Windows::Foundation::Collections::IVector<winrt::hstring>;
        using stdstring_vector = std::vector<std::string>;

        /// <summary>
        /// The costructor
        /// </summary>
        /// <param name="options">The options to initialize both the internal'tokenizer' and 'ctranslate2' libraries.</param>
        Translator(const TranslatorOptions& options);

        /// <summary>
        /// The costruction factory
        /// </summary>
        /// <param name="options">The options to initialize both the internal'tokenizer' and 'ctranslate2' libraries.</param>
        /// <returns>The Translator instance.</returns>
        static winrt::Windows::Foundation::IAsyncOperation<winrt::Paoldev::OfflineTextTranslator::ITranslator> CreateAsync(const TranslatorOptions& options);

        /// <summary>
        /// Translate a string from source language to destination language.
        /// sourceLanguage and destLanguage MUST be already formatted with model-specific prefixes; they may be empty.
        /// Exceptions may be thrown.
        /// </summary>
        /// <param name="sourceText">the source string to be translated.</param>
        /// <param name="sourceLanguage">the source string language, already formatted with model-specific prefix; it may be empty.</param>
        /// <param name="destLanguage">the desired translation language, already formatted with model-specific prefix; it may be empty.</param>
        /// <returns>The translated string.</returns>
        winrt::Windows::Foundation::IAsyncOperation<winrt::hstring> TranslateAsync(const hstring& sourceText, const hstring& sourceLanguage, const hstring& destLanguage);

        /// <summary>
        /// Translate many strings from source language to destination language.
        /// sourceLanguage and destLanguage MUST be already formatted with model-specific prefixes; they may be empty.
        /// Exceptions may be thrown.
        /// </summary>
        /// <param name="sourceTexts">the source strings to be translated.</param>
        /// <param name="sourceLanguage">the source strings language, already formatted with model-specific prefix; it may be empty.</param>
        /// <param name="destLanguage">the desired translation language, already formatted with model-specific prefix; it may be empty.</param>
        /// <returns>The translated strings; the output array size matches sourceTexts size.</returns>
        winrt::Windows::Foundation::IAsyncOperation<hstring_ivector> TranslateAsync(const hstring_ivector& sourceTexts, const hstring& sourceLanguage, const hstring& destLanguage);

        /// <summary>
        /// Translate a string from source language to many destination languages.
        /// sourceLanguage and destLanguages MUST be already formatted with model-specific prefixes; they may be empty.
        /// Exceptions may be thrown.
        /// </summary>
        /// <param name="sourceText">the source string to be translated.</param>
        /// <param name="sourceLanguage">the source string language, already formatted with model-specific prefix; it may be empty.</param>
        /// <param name="destLanguages">the desired translation languages, already formatted with model-specific prefix; it may be an empty array or some of its elements may be empty.</param>
        /// <returns>The string translated to many languages; the output array size matches destLanguages size.</returns>
        winrt::Windows::Foundation::IAsyncOperation<hstring_ivector> TranslateAsync(const hstring& sourceText, const hstring& sourceLanguage, const hstring_ivector& destLanguages);

        /// <summary>
        /// Translate many strings, each one with a specific source language, to the same destination language.
        /// sourceLanguages and destLanguage MUST be already formatted with model-specific prefixes; they may be empty.
        /// sourceLanguages array can be empty; otherwise its size MUST match sourceTexts size.
        /// Exceptions may be thrown.
        /// </summary>
        /// <param name="sourceTexts">the source strings to be translated.</param>
        /// <param name="sourceLanguages">the source strings language, already formatted with model-specific prefix; it may be empty, otherwise its size MUST match sourceTexts size.</param>
        /// <param name="destLanguage">the desired translation language, already formatted with model-specific prefix; it may be empty.</param>
        /// <returns>The strings all translated to the same language; the output array size matches sourceTexts size.</returns>
        winrt::Windows::Foundation::IAsyncOperation<hstring_ivector> TranslateAsync(const hstring_ivector& sourceTexts, const hstring_ivector& sourceLanguages, const hstring& destLanguage);

        /// <summary>
        /// Translate many strings, each one with a specific source language, to a specific destination language each one.
        /// sourceLanguages and destLanguages MUST be already formatted with model-specific prefixes; they may be empty.
        /// sourceLanguages and/or destLanguages array can be empty; otherwise their size MUST match sourceTexts size.
        /// Exceptions may be thrown.
        /// </summary>
        /// <param name="sourceTexts">the source strings to be translated.</param>
        /// <param name="sourceLanguages">the source strings language, already formatted with model-specific prefix; it may be an empty array, otherwise its size MUST match sourceTexts size and some of its elements may be empty.</param>
        /// <param name="destLanguages">the desired translation languages, already formatted with model-specific prefix; it may be an empty array, otherwise its size MUST match sourceTexts size and some of its elements may be empty.</param>
        /// <returns>The strings translated to their specific destination language; the output array size matches sourceTexts size.</returns>
        winrt::Windows::Foundation::IAsyncOperation<hstring_ivector> TranslateAsync(const hstring_ivector& sourceTexts, const hstring_ivector& sourceLanguages, const hstring_ivector& destLanguages);

        /// <summary>
        /// Translate many strings, each one with a specific source language, to all specified destination languages.
        /// sourceLanguages and destLanguages MUST be already formatted with model-specific prefixes; they may be empty.
        /// sourceLanguages array can be empty; otherwise its size MUST match sourceTexts size.
        /// destLanguages array can be empty or it can be of any size.
        /// Exceptions may be thrown.
        /// </summary>
        /// <param name="sourceTexts">the source strings to be translated.</param>
        /// <param name="sourceLanguages">the source strings language, already formatted with model-specific prefix; it may be empty, otherwise its size MUST match sourceTexts size.</param>
        /// <param name="destLanguages">the desired translation languages for each source string.</param>
        /// <returns>The strings each one translated to all specified destination languages; the output array size is the product of sourceTexts size and destLanguages size. To access the N-th translation of the M-th source string, the required index is "M * sourceTexts.size() + N" (where M and N are zero-based indices).
        /// If destLanguages is empty, the output array size matches sourceTexts size and each source string has just one translation (as if destLanguages was an array of 1 element containing an empty string as destination language).</returns>
        winrt::Windows::Foundation::IAsyncOperation<hstring_ivector> MultiTranslateAsync(const hstring_ivector& sourceTexts, const hstring_ivector& sourceLanguages, const hstring_ivector& destLanguages);

    private:

        //TODO: use std::string_view when a suitable interface will be available in low level libraries.
        void TranslateInternal(const stdstring_vector& InBatches, const stdstring_vector& sourceLanguages, const stdstring_vector& destLanguages, std::vector<hstring>& OutTranslatedBatches, const bool InMultiTranslate = false) const;

        std::unique_ptr<onmt::Tokenizer> m_tokenizer;
        std::unique_ptr<ctranslate2::Translator> m_translator;
    };
}

namespace winrt::Paoldev::OfflineTextTranslator::factory_implementation
{
    struct Translator : TranslatorT<Translator, implementation::Translator>
    {
    };
}
