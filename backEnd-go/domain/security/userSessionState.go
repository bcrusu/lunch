package security

type UserSessionState int

const (
	UserSessionStateActive UserSessionState = 0
	UserSessionStateClosed UserSessionState = 1
)
