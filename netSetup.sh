#!/bin/sh

# Подставьте вашу версию линукса. Так как в проекте используется 5-ая версия .net sdk
# то она работает не на всех версиях. Для переменной ver я перечислил версии системы, на которой она встает нормально
# Так же эта версия нужна для того, чтобы установить необходимые ключи и добавить их в список доверенных ключей  -> https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#2004-
ver=20.04 #18.04 16.04

wget https://packages.microsoft.com/config/ubuntu/$ver/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-5.0
sudo apt-get install -y dotnet-runtime-5.0
sudo apt-get install -y aspnetcore-runtime-5.0
