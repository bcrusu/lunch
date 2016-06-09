package configuration

import "time"

const day = 24 * time.Hour

const SessionExpireDays = 14 * day
const MaxActiveSessionsCount = 10

const JWTIssuer = "lunch.com"
const JWTAudience = "lunch"
const JWTSignatureAlgorithm = "SHA256"
