package businessLogic

import "github.com/bcrusu/lunch/backEnd-go/domain"

type UserSessionBusinessLogic interface {
	FindSession(token string) security.UserSession

	CreateSessionForExternalUser(externalUserDetails ExternalUserDetails) domain.UserSession

	CloseSession(userSession domain.UserSession)

	//TODO GetIsPrincipalValid(ClaimsPrincipalprincipal) bool

	//TODO GetUserSessionForPrincipal(ClaimsPrincipal principal) domain.UserSession
}
