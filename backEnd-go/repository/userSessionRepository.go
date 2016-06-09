package repository

import (
	"database/sql"

	"github.com/bcrusu/lunch/backEnd-go/domain"
	"github.com/bcrusu/lunch/backEnd-go/repository/internal"
)

type UserSessionRepository interface {
	Add(userSession domain.UserSession)
	FindByToken(token []byte) *domain.UserSession
	GetActiveUserSessionsCount(userID int64) int64
}

type userSessionRepository struct {
	db *sql.DB
}

func NewUserSessionRepository(db *sql.DB) UserSessionRepository {
	result := new(userSessionRepository)
	result.db = db
	return result
}

func (r *userSessionRepository) Add(userSession domain.UserSession) {
	internal.ExecInsert(r.db, `INSERT INTO [UserSessions] ([Token], [CreationDate], [State], [UserId]) 
		VALUES (?, ?, ?, ?)`, userSession.Token, userSession.CreationDate, userSession.State, userSession.UserID)
}

func (r *userSessionRepository) FindByToken(token []byte) *domain.UserSession {
	entity := internal.RunQueryRow(r.db, userSessionFactory, selectUserSessionBase+" WHERE us.[Token] = ?", token)
	return entity.(*domain.UserSession)
}

func (r *userSessionRepository) GetActiveUserSessionsCount(userID int64) int64 {
	value := internal.ExecScalar(r.db, "SELECT count(*) FROM [UserSessions] us WHERE us.[UserId] = ? AND us.[State] = ?", userID, domain.UserSessionStateActive)
	return value.(int64)
}

func userSessionFactory() (interface{}, []interface{}) {
	instance := domain.UserSession{}
	scanDest := []interface{}{&instance.Token,
		&instance.UserID,
		&instance.CreationDate,
		&instance.State}

	return &instance, scanDest
}

const selectUserSessionBase = `SELECT [Token],
    [UserId], 
	[CreationDate],
	[State] FROM [UserSessions] us`
