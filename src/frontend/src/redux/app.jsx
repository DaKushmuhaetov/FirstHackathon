const UPDATE_TOKEN = 'UPDATE_TOKEN'

export function updateToken(token) {
    return {
        type: UPDATE_TOKEN,
        payload: token
    }
}

const initialState = {
    token: ''
}

export const app = (state = initialState, action) => {
    switch (action.type) {
        case UPDATE_TOKEN:
            return { ...state, token: action.payload }
        default:
            return state
    }
}