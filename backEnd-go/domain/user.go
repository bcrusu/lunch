package domain

type User struct {
	ID          int64
	ExternalID  string
	Type        UserType
	Email       string
	FirstName   string
	LastName    string
	DisplayName string
	Description string
	PictureURL  string
}
