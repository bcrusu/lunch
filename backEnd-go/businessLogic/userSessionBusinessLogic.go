package businessLogic

import (
	"database/sql"
	"fmt"
	"time"

	"github.com/bcrusu/lunch/backEnd-go/configuration"
	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository"
	"github.com/satori/go.uuid"
)

type UserSessionBusinessLogic interface {
	FindSession(token []byte) *domain.UserSession
	CreateSessionForExternalUser(externalUserDetails ExternalUserDetails) *domain.UserSession
	CloseSession(userSession *domain.UserSession)
	//TODO GetIsPrincipalValid(ClaimsPrincipalprincipal) bool
	//TODO GetUserSessionForPrincipal(ClaimsPrincipal principal) domain.UserSession
}

func NewUserSessionBusinessLogic(db *sql.DB) UserSessionBusinessLogic {
	result := new(userSessionBusinessLogic)
	result.userSessionRepository = repository.NewUserSessionRepository(db)
	result.userBusinessLogic = NewUserBusinessLogic(db)
	return result
}

type userSessionBusinessLogic struct {
	userSessionRepository repository.UserSessionRepository
	userBusinessLogic     UserBusinessLogic
}

func (r *userSessionBusinessLogic) FindSession(token []byte) *domain.UserSession {
	return r.userSessionRepository.FindByToken(token)
}

func (r *userSessionBusinessLogic) CreateSessionForExternalUser(externalUserDetails ExternalUserDetails) *domain.UserSession {
	user := r.userBusinessLogic.FindByEmail(externalUserDetails.Email)
	if user == nil {
		user = r.userBusinessLogic.CreateUser(externalUserDetails)
	} else {
		if user.Type != externalUserDetails.UserType {
			panic(fmt.Errorf("Multiple sign-in providers are not supported for the same user."))
		}

		if !r.checkActiveUserSessionsCount(user.ID) {
			panic(fmt.Errorf("Max number of active sessions reached for user %d", user.ID))
		}

		// Update user information
		user.FirstName = externalUserDetails.FirstName
		user.LastName = externalUserDetails.LastName
		user.DisplayName = externalUserDetails.DisplayName
		user.PictureURL = externalUserDetails.PictureURL

		r.userBusinessLogic.UpdateUser(user)
	}

	session := new(domain.UserSession)
	session.Token = uuid.NewV4().Bytes()
	session.UserID = user.ID
	session.State = domain.UserSessionStateActive
	session.CreationDate = time.Now()

	r.userSessionRepository.Add(session)
	return session
}

func (r *userSessionBusinessLogic) CloseSession(userSession *domain.UserSession) {
	userSession.State = domain.UserSessionStateClosed
	r.userSessionRepository.Update(userSession)
}

func (r *userSessionBusinessLogic) checkActiveUserSessionsCount(userID int64) bool {
	sessions := r.userSessionRepository.GetActiveUserSessions(userID)

	count := 0
	for _, session := range sessions {
		if !isSessionValid(session) {
			continue
		}

		count++
		if count < configuration.MaxActiveSessionsCount {
			continue
		}

		return false
	}

	return true
}

func isSessionValid(userSession domain.UserSession) bool {
	if userSession.State != domain.UserSessionStateActive {
		return false
	}

	expireDate := userSession.CreationDate.Add(configuration.SessionExpireDays)
	if expireDate.Before(time.Now()) {
		return false
	}

	return true
}
