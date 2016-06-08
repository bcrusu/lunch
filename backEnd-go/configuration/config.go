package configuration

import (
	"encoding/base64"
	"fmt"

	"github.com/spf13/viper"
)

var publicConfig *viper.Viper
var secretConfig *viper.Viper

var cachedJwtSignKey []byte

func init() {
	publicConfig = createConfig(".")
	secretConfig = createConfig("$HOME/.lunch")
}

func createConfig(configPath string) *viper.Viper {
	result := viper.New()
	result.SetConfigName("config")
	result.AddConfigPath(configPath)

	err := result.ReadInConfig()
	if err != nil {
		panic(fmt.Errorf("Fatal error config file: %s", err))
	}

	return result
}

func DefaultAccessControlAllowOrigin() string {
	return publicConfig.GetString("DefaultAccessControlAllowOrigin")
}

func ConnectionString() string {
	return secretConfig.GetString("ConnectionString")
}

func LinkedinClientSecret() string {
	return secretConfig.GetString("LinkedinClientSecret")
}

func JwtSignKey() []byte {
	if cachedJwtSignKey != nil {
		return cachedJwtSignKey
	}

	base64Str := secretConfig.GetString("JwtSignKey")

	res, err := base64.StdEncoding.DecodeString(base64Str)
	if err != nil {
		panic(fmt.Errorf("Invalid config value detected. Could not decode JwtSignKey value. %s", err))
	}

	cachedJwtSignKey = res
	return cachedJwtSignKey
}
