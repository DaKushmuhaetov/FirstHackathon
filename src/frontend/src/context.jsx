import {createContext} from 'react'

export const Context = createContext({
    handleToast: function() {},
    login: function() {},
    logout: function() {},
    token: '',
    isAuthed: false,
    parseJwt: function() {}
})