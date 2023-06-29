#!/bin/bash

token=null
app_version=85
api="https://api.notvpn.io"
user_agent="okhttp/4.9.2"
operation_system="android"
device_id="$(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 16 | head -n 1)"

function login() {
	# 1 - token: (string): <token>
	token=$1
	curl --request POST \
		--url "$api/users/autorisation" \
		--user-agent "$user_agent" \
		--header "content-type: application/x-www-form-urlencoded" \
		--data "token=$token&v=$app_version&os=$operation_system&versionOs=28&deviceName=RMX3551"
}

function register() {
	curl --request POST \
		--url "$api/users/registration" \
		--user-agent "$user_agent" \
		--header "content-type: application/x-www-form-urlencoded" \
		--data "mail=false&language=ru&languageOriginal=ru&languageOriginalTeg=ru-RU&countryUser=ru&json={"full":false,"elements":["instagram","facebook","twitter","youtubeimage"]}&os=$operation_system&v=$app_version&litel=true&deviceId=$device_id"
}

function get_servers() {
	curl --request POST \
		--url "$api/ping/get_servers" \
		--user-agent "$user_agent" \
		--header "content-type: application/x-www-form-urlencoded" \
		--data "token=$token&counrty_list=[\"DE\",\"CA\",\"FI\",\"US\",\"NL\",\"SG\",\"RU\",\"UA\",\"BY\",\"TR\",\"AU\",\"KZ\",\"ES\",\"PL\",\"FR\",\"SE\",\"CH\",\"EE\",\"NO\",\"BG\",\"RO\",\"DK\",\"CZ\",\"GB\"]&rate=true&v=$app_version"
}
