package repository

import (
	"database/sql"

	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository/internal"
)

type UserSessionRepository interface {
	Add(userSession *domain.UserSession)
	Update(userSession *domain.UserSession)
	FindByToken(token []byte) *domain.UserSession
	GetActiveUserSessions(userID int64) []domain.UserSession
}

type userSessionRepository struct {
	db *sql.DB
}

func NewUserSessionRepository(db *sql.DB) UserSessionRepository {
	result := new(userSessionRepository)
	result.db = db
	return result
}

func (r *userSessionRepository) Add(userSession *domain.UserSession) {
	internal.ExecInsert(r.db, `INSERT INTO [UserSessions] ([Token], [CreationDate], [State], [UserId]) 
		VALUES (?, ?, ?, ?)`, userSession.Token, userSession.CreationDate, userSession.State, userSession.UserID)
}

func (r *userSessionRepository) Update(userSession *domain.UserSession) {
	internal.ExecUpdate(r.db, `UPDATE [UserSessions] SET [CreationDate] = ?, [State] = ?, [UserId] = ? 
		WHERE [Token] = ?`, userSession.CreationDate, userSession.State, userSession.UserID, userSession.Token)
}

func (r *userSessionRepository) FindByToken(token []byte) *domain.UserSession {
	entity := internal.RunQueryRow(r.db, userSessionFactory, selectUserSessionBase+" WHERE us.[Token] = ?", token)
	return entity.(*domain.UserSession)
}

func (r *userSessionRepository) GetActiveUserSessions(userID int64) []domain.UserSession {
	entities := internal.RunQuery(r.db, userSessionFactory, selectUserSessionBase+" WHERE us.[UserId] = ? AND us.[State] = ?", userID, domain.UserSessionStateActive)
	return getUserSessions(entities)
}

func userSessionFactory() (interface{}, []interface{}) {
	instance := domain.UserSession{}
	scanDest := []interface{}{&instance.Token,
		&instance.UserID,
		&instance.CreationDate,
		&instance.State}

	return &instance, scanDest
}

func getUserSessions(entities []interface{}) []domain.UserSession {
	result := make([]domain.UserSession, len(entities))
	for _, entity := range entities {
		result = append(result, entity.(domain.UserSession))
	}

	return result
}

const selectUserSessionBase = `SELECT [Token],
    [UserId], 
	[CreationDate],
	[State] FROM [UserSessions] us`
