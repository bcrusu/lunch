package domain

import "time"

//TOOD: github.com/satori/go.uuid

type UserSession struct {
	Token        string
	UserID       int
	CreationDate time.Time
	State        UserSessionState
}
