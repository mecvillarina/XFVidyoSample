## Xamarin Forms - Vidyo.io sample app [![PayPal donate button](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4KHTXCBWYXTNG "Donate to this project using Paypal")

Vidyo.io (https://vidyo.io/) - Rapidly embed real-time group video chat into your app.

This sample app was made using Xamarin Forms and uses XFVidyo nuget library that compressed the Vidyo.io connector for xamarin sdks in a single nuget library.

XFVidyo Nuget: https://www.nuget.org/packages/XFVidyo

vidyo.io-connector-xamarin: https://github.com/Vidyo/vidyo.io-connector-xamarin

Vidyo.io iOS SDK package: https://static.vidyo.io/latest/package/VidyoClient-iOSSDK.zip

Vidyo.io Android SDK package: https://static.vidyo.io/latest/package/VidyoClient-AndroidSDK.zip

## Usage:

Get your key and app-id in the vidyo.io developer portal (https://developer.vidyo.io) and update the following file

```C#

//XFVidyoSample/XFVidyoSample/Common/VidyoConstants.cs

public const string Key = "vidyo-io-api-key";
public const string AppId = "vidyo-io-app-id";
```

Also, make sure that in your iosProject.csproj, you have added the following configuration for the native iOS linker.

```xml
<ItemGroup>
    <NativeReference Include="Native References/libcrypto.a">
      <Kind>Static</Kind>
      <SmartLink>False</SmartLink>
    </NativeReference>
    <NativeReference Include="Native References/libopus.a">
      <Kind>Static</Kind>
      <SmartLink>False</SmartLink>
    </NativeReference>
    <NativeReference Include="Native References/libspeex.a">
      <Kind>Static</Kind>
      <SmartLink>False</SmartLink>
    </NativeReference>
    <NativeReference Include="Native References/libsrtp.a">
      <Kind>Static</Kind>
      <SmartLink>False</SmartLink>
    </NativeReference>
    <NativeReference Include="Native References/libssl.a">
      <Kind>Static</Kind>
      <SmartLink>False</SmartLink>
    </NativeReference>
    <NativeReference Include="Native References/libVidyoClient.a">
      <Kind>Static</Kind>
      <SmartLink>False</SmartLink>
      <Frameworks>AudioToolbox AVFoundation CoreLocation CoreMedia SystemConfiguration UIKit</Frameworks>
    </NativeReference>
    <NativeReference Include="Native References/VPX.framework">
      <Kind>Framework</Kind>
      <SmartLink>False</SmartLink>
    </NativeReference>
  </ItemGroup>

```