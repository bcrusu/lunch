package main

import (
	"log"
	"net/http"
)

func main() {

	mux := http.NewServeMux()

	rh := http.RedirectHandler("http://example.org", 307)
	mux.Handle("/foo", rh)
	mux.Handle("/hi", &myHandler{Name: "bill"})

	log.Println("Listening...")
	http.ListenAndServe("localhost:3000", mux)
}

type myHandler struct {
	Name string
}

func (r *myHandler) ServeHTTP(responseWriter http.ResponseWriter, request *http.Request) {
	responseWriter.Write([]byte("hi!"))
	responseWriter.Write([]byte(r.Name))
}
