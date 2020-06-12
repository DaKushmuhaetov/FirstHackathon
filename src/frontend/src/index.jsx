import React from 'react'
import ReactDOM from 'react-dom'
import App from './components/App'

// Router
import { BrowserRouter } from 'react-router-dom'
const history = require('history').createBrowserHistory()

ReactDOM.render(
    <BrowserRouter history={history}>
        <App />
    </BrowserRouter>,
    document.getElementById('root')
)
