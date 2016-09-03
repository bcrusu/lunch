import { fork } from 'redux-saga/effects'
import { watchPublicRoot } from './public/publicSagas'
import { watchProfileRoot } from './profile/profileSagas'

export default function* () {
  console.log('root saga is running...')

  yield [
    fork(watchPublicRoot),
    fork(watchProfileRoot)
  ]
}
