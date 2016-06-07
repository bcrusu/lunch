package handlers

import (
	"fmt"
	"net/http"

	"goji.io/pat"
	"golang.org/x/net/context"
)

func SignInLinkedin(ctx context.Context, w http.ResponseWriter, r *http.Request) {
	//TODO
	name := pat.Param(ctx, "name")
	fmt.Fprintf(w, "Hello, %s!", name)
}

func SignOutCurrentSession(ctx context.Context, w http.ResponseWriter, r *http.Request) {
	//TODO
}
