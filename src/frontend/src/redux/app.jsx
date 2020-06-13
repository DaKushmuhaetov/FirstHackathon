const UPDATE_DATA = 'UPDATE_DATA'

export function updateData(data) {
    return {
        type: UPDATE_DATA,
        payload: data
    }
}

const initialState = {
    data: {}
}

export const app = (state = initialState, action) => {
    switch (action.type) {
        case UPDATE_DATA:
            return { ...state, data: action.payload }
        default:
            return state
    }
}