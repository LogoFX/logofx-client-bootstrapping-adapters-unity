SET package_name=LogoFX.Client.Bootstrapping.Adapters.Unity
SET package_version=2.2.2
cd ../build
call build.bat
cd ../pack
call ./pack.bat
cd ../publish
call ./copy.bat %package_name% %package_version% %1
cd ../install
call uninstall-global-single.bat %package_name% %package_version%
cd ..