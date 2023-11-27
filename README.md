# Spectre.Net
A port of the [Spectre password generation](https://gitlab.com/spectre.app/) API to .NET with some (incomplete) programs built on top

## Project Structure:

- Spectre.Cli - A .NET CLI program using the coincidentally named Spectre console library for some pretty output
- Spectre.Net - A .NET MAUI GUI program attempting to use good MVVM principles
- Spectre.Linux - An Avalonia GUI program attempting to share the same MVVM principles, aimed primarily at Linux since MAUI doesn't support that platform (but will run on other platforms too)
- Spectre.Net.Api - The actual API that does the password generation based on the Spectre algorithm, using libsodium for some of the crypto not found in .NET
- Spectre.Shared - Stuff shared between Spectre.Net and Spectre.Linux (models and view models)

## License

Since this is obviously derived from the aforementioned GPL-3.0 licensed repository, this repository is thusly also GPL-3.0. 