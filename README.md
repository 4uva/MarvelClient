# MarvelClient
Simple client for Marvel API

The app is divided into several parts:
* SDK part which is responsible for Marvel API communication and storing the saved data
* Simple Xamarin application which is using SDK part
* Platform-specific parts created by Xamarin project wizard.

JSON.NET is used for serialization because it gives about 2x size advantage over built-in XML serialization.
ASP.NET Web API client library is used for deserialization of data coming from REST API.

The data are requested from the API only when they are about to be displayed to the user.
Until the data are downloaded, a dummy item is shown.

The data are stored in cache which is persisted in app properties across app runs using JSON serialization.
The requests are not sent if the data are already in cache.
The cache validity is 1 day (according to Marvel's recomendation).

The repository doesn't contain api keys (need to be entered before compilation).

The app is tested on Android emulator.

The development time is about 30 hours.
