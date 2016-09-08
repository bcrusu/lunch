export const getUserInfo = state => selectFromCache(state, cache => cache.userInfo)

function selectFromCache(state, selector) {
    let cache = state.api.cache
    return selector(cache)
}
