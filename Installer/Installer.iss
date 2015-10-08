; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "PLan2015"
#define MyAppExeName "Plan2015.Score.ScoreBoard.exe"
#define MyAppVersion GetFileVersion(SourcePath + "\Application\" + MyAppExeName)
#define MyAppPublisher "Bismuth"
#define MyAppURL "http://bismuth.dk"

#define DotNetSetup "dotNetFx40_Full_setup.exe"
#define XnaSetup "xnafx40_redist.msi"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{fa6f0758-a0c0-4b84-89e0-fbaa0fdfb0fb}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/about
AppUpdatesURL={#MyAppURL}/{#LowerCase(MyAppName)}
DefaultDirName={sd}\{#MyAppPublisher}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir=Output
OutputBaseFilename={#MyAppName}Setup
SetupIconFile=Installer.ico
UninstallDisplayIcon={app}\{#MyAppExeName}
Compression=lzma
SolidCompression=yes
WizardImageFile=Image.bmp
WizardSmallImageFile=ImageSmall.bmp

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "Dependencies\{#DotNetSetup}"; DestDir: {tmp}; Flags: deleteafterinstall; Check: CheckForFramework
Source: "Dependencies\{#XnaSetup}"; DestDir: {tmp}; Flags: deleteafterinstall
Source: "Application\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
;Name: "{group}\Configuration"; Filename: "{app}\Configuration.exe"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: {tmp}\{#DotNetSetup}; Parameters: "/q:a /c:""install /l /q"""; Check: CheckForFramework; StatusMsg: Microsoft .NET Framework 4.0 is being installed. Please wait...
Filename: msiexec.exe; Parameters: "/quiet /i ""{tmp}\{#XnaSetup}"""; StatusMsg: Microsoft XNA Framework 4.0 Refresh is being installed. Please wait...
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
// Check if .NET Framework 4.0 is installed.
Function CheckForFramework : boolean;
Var
regresult : cardinal;
Begin
RegQueryDWordValue(HKLM, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', regresult);
If regresult = 0 Then
Begin
Result := true;
End
Else
Result := false;
End;
