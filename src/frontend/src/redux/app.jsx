const UPDATE_DATA = 'UPDATE_DATA'

export function updateData(data) {
    return {
        type: UPDATE_DATA,
        payload: data
    }
}

const initialState = {
    data: {
        id: '0',
        name: 'Test',
        surname: 'Testovich',
        email: 'none',
        address: 'none'
    }
}

export const app = (state = initialState, action) => {
    switch (action.type) {
        case UPDATE_DATA:
            return { ...state, data: action.payload }
        default:
            return state
    }
}