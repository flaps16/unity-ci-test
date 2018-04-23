#! /bin/sh

# Download Unity3D installer into the container
#  The below link will need to change depending on the version - Various links included with version in the links
#  Refer to https://unity3d.com/get-unity/download/archive and find the link pointed to by Mac "Unity Editor"
echo 'Downloading Unity 2017.2.2f1 pkg:'
curl --retry 5 -o Unity.pkg https://download.unity3d.com/download_unity/1f4e0f9b6a50/MacEditorInstaller/Unity-2017.2.2f1.pkg

# http://netstorage.unity3d.com/unity/88d00a7498cd/MacEditorInstaller/Unity-5.5.1f1.pkg
# https://download.unity3d.com/download_unity/497a0f351392/MacEditorInstaller/Unity-5.6.0f3.pkg
# https://download.unity3d.com/download_unity/2860b30f0b54/MacEditorInstaller/Unity-5.6.1f1.pkg
# https://download.unity3d.com/download_unity/a2913c821e27/MacEditorInstaller/Unity-5.6.2f1.pkg
# https://download.unity3d.com/download_unity/d3101c3b8468/MacEditorInstaller/Unity-5.6.3f1.pkg
# https://download.unity3d.com/download_unity/ac7086b8d112/MacEditorInstaller/Unity-5.6.4f1.pkg
# https://download.unity3d.com/download_unity/2cac56bf7bb6/MacEditorInstaller/Unity-5.6.5f1.pkg
# https://download.unity3d.com/download_unity/472613c02cf7/MacEditorInstaller/Unity-2017.1.0f3.pkg
# https://download.unity3d.com/download_unity/5d30cf096e79/MacEditorInstaller/Unity-2017.1.1f1.pkg
# https://download.unity3d.com/download_unity/574eeb502d14/MacEditorInstaller/Unity-2017.1.3f1.pkg
# https://download.unity3d.com/download_unity/46dda1414e51/MacEditorInstaller/Unity-2017.2.0f3.pkg
# https://download.unity3d.com/download_unity/94bf3f9e6b5e/MacEditorInstaller/Unity-2017.2.1f1.pkg
# https://download.unity3d.com/download_unity/1f4e0f9b6a50/MacEditorInstaller/Unity-2017.2.2f1.pkg
# https://download.unity3d.com/download_unity/a9f86dcd79df/MacEditorInstaller/Unity-2017.3.0f3.pkg
# https://netstorage.unity3d.com/unity/fc1d3344e6ea/MacEditorInstaller/Unity-2017.3.1f1.pkg

if [ $? -ne 0 ]; then { echo "Download failed"; exit $?; } fi

# Run installer
echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
