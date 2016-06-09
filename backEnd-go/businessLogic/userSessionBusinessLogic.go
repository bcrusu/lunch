package businessLogic

import (
	"database/sql"

	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository"
)

//TOOD: github.com/satori/go.uuid

type UserSessionBusinessLogic interface {
	FindSession(token string) security.UserSession

	CreateSessionForExternalUser(externalUserDetails ExternalUserDetails) domain.UserSession

	CloseSession(userSession domain.UserSession)

	//TODO GetIsPrincipalValid(ClaimsPrincipalprincipal) bool

	//TODO GetUserSessionForPrincipal(ClaimsPrincipal principal) domain.UserSession
}

func NewUserSessionBusinessLogic(db *sql.DB) UserSessionBusinessLogic {
	result := new(userSessionBusinessLogic)
	result.userRepository = repository.NewUserRepository(db)
	return result
}

type userSessionBusinessLogic struct {
	userSessionRepository repository.UserSessionRepository
}
