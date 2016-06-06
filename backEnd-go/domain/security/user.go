package security

type User struct {
	ID          int
	ExternalID  string
	Type        UserType
	UserType    string
	FirstName   string
	LastName    string
	DisplayName string
	Description string
	PictureURL  string
}
