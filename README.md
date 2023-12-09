# Offline Text Translator

[![License: MIT](https://img.shields.io/badge/License-MIT-red.svg)](./LICENSE)

This project creates a simple .NET component to translate texts in offline mode on Windows.  
I wrote it some time ago for personal needs and I'm not involved with any third party library used here (all of them are built through [vcpkg](https://github.com/microsoft/vcpkg) build system).

This work is based on the following libraries and samples
* https://github.com/OpenNMT/Tokenizer
* https://github.com/OpenNMT/CTranslate2
* https://github.com/argosopentech/MetalTranslate
 
The code to create the C++/WinRT component and its .NET wrapper is based on  
* https://learn.microsoft.com/en-us/windows/apps/develop/platform/csharp-winrt/net-projection-from-cppwinrt-component
* https://github.com/microsoft/CsWinRT/tree/master/src/Samples/NetProjectionSample

This project is also a test for [vcpkg](https://github.com/microsoft/vcpkg) in Manifest mode  
* https://github.com/microsoft/vcpkg
* https://github.com/paoldev/vcpkg-custom-overlays

# How to use
In order to work, you have to manually download the models to initialize both the tokenizer and the translator itself, compatible with both [Tokenizer](https://github.com/OpenNMT/Tokenizer) and [CTranslate2](https://github.com/OpenNMT/CTranslate2) libraries: read their respective repositories for documentation.  
1. Clone the entire repository and its submodules  
    `git clone --recurse https://github.com/paoldev/Paoldev.OfflineTextTranslator`  
2. From a command prompt, go to the `vcpkg` folder and download the `vcpkg.exe` executable by running  
    `bootstrap-vcpkg.bat`
3. Download the models into any directory  
4. Create a `languages.txt` file anywhere to describe your model supported languages.  
    I don't know if it's possible to extract such information from model files; so it was easier for me to write and parse such `languages.txt` file.  
    The template file format is as in the following examples; use `language` as a keyword for the `language_prefix` entry.  
    
    **Sample 1**  
    If the model requires string prefixes such as `{en}`, `{fr}`, etc. as language prefix to translate text entries, use this template:  
    ```
    language_prefix:{language}
    english_code_for_ui:en
    de
    en
    es
    fr
    it
    ```  
    **Sample 2**  
    If the model requires string prefixes such as `__en__`, `__fr__`, etc. as language prefix to translate text entries, use this template:  
    ```
    language_prefix:__language__
    english_code_for_ui:en
    de
    en
    es
    fr
    it
    ```
    **Sample 3**  
    If the model doesn't require any language prefix to translate text entries, maybe because the model only translates texts from one language (`de`) to another one (`fr`), this template can be used:  
    ```
    language_prefix:
    english_code_for_ui:
    de
    fr
    ```
5. Open the solution `Paoldev.OfflineTextTranslator.sln`  
6. Set `OfflineTextTranslatorApp` as Startup Project.  
7. Compile `OfflineTextTranslatorProjection`: it may require a lot of time to compile its c++ dependencies through vcpkg.  
8. Compile `OfflineTextTranslatorApp` sample and then run it.  
9. Select your model files through `File -> Configuration` dialog box.  
10. Settings will be saved in `%localappdata%\paoldev\OfflineTextTranslatorApp` folder (or similar name).  
    In case of problems running the application, delete the `user.config` file from that folder (or subfolder) and then restart the application.  
    
`OfflineTextTranslatorApp` requires the [.NET 8.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0); it can be automatically downloaded and installed on the application's first run.  

# License

This project is licensed under the terms of the [MIT license](./LICENSE).  
The libraries provided by vcpkg ports are licensed under the terms of their original authors.

