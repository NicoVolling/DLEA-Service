fx_version 'cerulean'
games { 'rdr3', 'gta5' }

author 'Nico Volling'
description 'DLEA-Service'
version '2.0.0'

files {
 "NativeUI.dll",
 "Newtonsoft.Json.dll"
}

client_scripts {
    "Client.net.dll",
    "DLEA_Lib.net.dll",
    "Newtonsoft.Json.dll"
}

server_script {
    "Server.net.dll",
    "DLEA_Lib.net.dll"
}