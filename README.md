# Tesla-for-Tizen
A Tizen Wearable app for your Tesla Vehicle.

# Project Setup

You need to add 2 files to ` Tesla for Tizen/res/`.

I will eventually remove `developer.json`, but it helps me move a lot quicker without hitting the Tesla API all the time, until I get proper caching built out.

`developer.json`
```
{
  "accessToken": "<accessToken>",
  "tokenType": "<tokenType>",
  "expiresIn": 0,
  "createdAt": 0,
  "refreshToken": "<refreshToken>",
  "email": "<email>",
  "password": "<password>"
}
```

`teslaApiClient.json`
```
{
  "clientId": "<clientId>",
  "clientSecret": "<clientSecret>"
}
```

# Special Thanks

https://tesla-api.timdorr.com/vehicle/commands/wake

https://github.com/JSkimming/tesla-net
