package domain

import "time"

type UserSession struct {
	Token        []byte
	UserID       int64
	CreationDate time.Time
	State        UserSessionState
}
