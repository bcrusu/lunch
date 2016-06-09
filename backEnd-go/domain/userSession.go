package domain

import "time"

type UserSession struct {
	Token        []byte
	UserID       int
	CreationDate time.Time
	State        UserSessionState
}
