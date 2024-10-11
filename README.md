# Spherum.Mobile.App


A .NET MAUI Cross Platform application written in C#.

- Targeting Android 7.0 (API 24) and above.
- Targeting iOS 13 and above.

This application is an assignment given by `Spherum` and provides student management functionality for `Spherum`.


## Solution Structure


```mermaid
flowchart TD

    A(Spherum.Mobile.Common) -->|Referenced by| B(Spherum.Mobile.Services)
    A(Spherum.Mobile.Common) -->|Referenced by| C(Spherum.Mobile)
    A(Spherum.Mobile.Common) --> |Referenced by| D(Spherum.Mobile.UnitTests)
    A(Spherum.Mobile.Common) --> |Referenced by| F(Spherum.Mobile.ViewModels)
    B(Spherum.Mobile.Services) --> |Referenced by| C(Spherum.Mobile)
    B(Spherum.Mobile.Services) -->|Referenced by| D(Spherum.Mobile.UnitTests)
    B(Spherum.Mobile.Services) -->|Referenced by| F(Spherum.Mobile.ViewModels)
    F(Spherum.Mobile.ViewModels) --> |Referenced by| C(Spherum.Mobile)
    F(Spherum.Mobile.ViewModels) --> |Referenced by| D(Spherum.Mobile.UnitTests)

```
