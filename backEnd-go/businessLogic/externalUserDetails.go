package businessLogic

import "github.com/bcrusu/lunch/backEnd-go/domain"

type ExternalUserDetails struct {
	UserType    security.UserType
	ID          string
	Email       string
	FirstName   string
	LastName    string
	DisplayName string
	Description string
	PictureURL  string
}
