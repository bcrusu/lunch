package security

import "github.com/bcrusu/lunch/backEnd-go/domain/security"

type UserSessionBusinessLogic interface {
	FindSession(token string) security.UserSession

	CreateSessionForExternalUser(externalUserDetails ExternalUserDetails) security.UserSession

	CloseSession(userSession security.UserSession)

	//TODO GetIsPrincipalValid(ClaimsPrincipalprincipal) bool

	//TODO GetUserSessionForPrincipal(ClaimsPrincipal principal) security.UserSession
}
