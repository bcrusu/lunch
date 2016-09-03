import { createStore, applyMiddleware, compose } from 'redux'
import createLogger from 'redux-logger'
import rootReducer from './rootReducer'
import rootSaga from './rootSaga'
import createSagaMiddleware from 'redux-saga'

const isProd = process.env.NODE_ENV === 'production'
const isDev = !isProd

export default function configureStore(preloadedState) {
  const { enhancer, runSaga } = getEnhancer()
  const store = createStore(
    rootReducer,
    preloadedState,
    enhancer
  )

  if (isDev)
    enableHotModuleReloading(store)

  runSaga(rootSaga)

  return store
}

function getEnhancer() {
  const sagaMiddleware = createSagaMiddleware()
  let middleware = [sagaMiddleware]

  if (isDev)
    middleware = [...middleware, createLogger()]

  let enhancer = applyMiddleware(...middleware)

  if (isDev) {
    let devToolsExtension = window.devToolsExtension ? window.devToolsExtension() : f => f
    enhancer = compose(enhancer, devToolsExtension)
  }

  return { enhancer, runSaga: sagaMiddleware.run }
}

function enableHotModuleReloading(store) {
  if (module.hot) {
    // TODO: review hmr - should explicitly include the public/profile reducers also? 
    // Enable Webpack hot module replacement for reducers
    module.hot.accept('./rootReducer', () => {
      const nextRootReducer = require('./rootReducer').default
      store.replaceReducer(nextRootReducer)
    })
  }
}