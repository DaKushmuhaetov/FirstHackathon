import React from 'react'
import ReactDOM from 'react-dom'
import App from './components/App'

// Redux
import { createStore } from 'redux'
import { Provider } from 'react-redux'
import { app } from './redux/app'

// Router
import { BrowserRouter } from 'react-router-dom'

const history = require('history').createBrowserHistory()
const store = createStore(app)

ReactDOM.render(
    <BrowserRouter history={history}>
        <Provider store={store}>
            <App />
        </Provider>
    </BrowserRouter>,
    document.getElementById('root')
)
