package handlers

import (
	"goji.io"
	"goji.io/pat"
)

const baseAddress = "/api"

func ConfigureMux(mux *goji.Mux) {
	mux.HandleFuncC(get("account/SignInLinkedin"), SignInLinkedin)
	mux.HandleFuncC(get("account/SignOutCurrentSession"), SignOutCurrentSession)

	mux.HandleFuncC(get("userProfile/UserInfo"), UserInfo)
}

func get(suffix string) goji.Pattern {
	return pat.Get(baseAddress + "/" + suffix)
}
