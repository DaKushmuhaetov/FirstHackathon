// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'
import {Link as LinkRouter} from 'react-router-dom'

// MaterialUI
import Avatar from '@material-ui/core/Avatar'
import Button from '@material-ui/core/Button'
import CssBaseline from '@material-ui/core/CssBaseline'
import TextField from '@material-ui/core/TextField'
import Box from '@material-ui/core/Box'
import Typography from '@material-ui/core/Typography'
import { withStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import HomeIcon from '@material-ui/icons/Home'

// Context
import {Context} from '../../context'

// Modules
import Http from '../../modules/http'

// Styles
import './index.css'

const styles = theme => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },

    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },

    form: {
        width: '100%',
        marginTop: theme.spacing(1),
    },

    submit: {
        margin: theme.spacing(3, 0, 2),
    },

    link: {
        "&:hover": {
            textDecoration: 'underline',
        },
    },
})

class Login extends React.PureComponent {
    static contextType = Context

    state = {
        email: '',
        password: '',

        invalidList: []
    }

    handleChange = (e) => {
        e.preventDefault()

        const val = e.target.value
        const name = e.target.name

        this.setState({ [name]: val })
    }

    handleValidate = () => {
        let invalidList = []

        const withoutSymbols = /^([a-zа-яё]+|\d+)$/i

        if (withoutSymbols.test(this.state.password) === false) {
            invalidList.push('password')
        }

        const email = /\S+@\S+\.\S+/
        if (email.test(this.state.email) === false) {
            invalidList.push('email')
        }

        this.setState({ invalidList }, () => console.log(this.state.invalidList))

        return invalidList.length > 0 ? invalidList : true
    }

    handleAuth = async (e) => {
        e.preventDefault()

        if (this.handleValidate() !== true) return

        const data = JSON.stringify({
            login: this.state.email,
            password: this.state.password
        })

        let http = new Http(`/person/login`, 'POST', data, { 'Content-Type': 'application/json' })

        const response = await http.request().catch(() => {
            this.context.handleToast('Нет ответа от сервера: вход', '#DC143C', 5000)
            return
        })

        console.log(response)
    }

    render() {
        const {classes} = this.props
        const invalidList = this.state.invalidList

        return (
            <Container component="div" maxWidth="xs" className="auth">
                <CssBaseline />
                <div className={classes.paper}>
                    <Avatar className={classes.avatar}>
                        <HomeIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Вход
                    </Typography>
                    <form style={{ textAlign: 'center' }} className={classes.form} noValidate>
                        <TextField
                            error={invalidList.includes('email')}

                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email адрес"
                            name="email"
                            autoComplete="email"
                            autoFocus

                            onChange={this.handleChange}
                            value={this.state.email}
                        />
                        <TextField
                            error={invalidList.includes('password')}

                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Пароль"
                            type="password"
                            id="password"
                            autoComplete="current-password"

                            onChange={this.handleChange}
                            value={this.state.password}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}

                            onClick={this.handleAuth}
                        >
                            Вход
                        </Button>
                        <LinkRouter className={classes.link} to="/registration">Нет аккаунта? Зарегистрируйтесь</LinkRouter>
                    </form>
                </div>
                <Box mt={8}>
                    <Typography variant="body2" color="textSecondary" align="center">
                        {'Copyright ©'}
                        {' '}
                        <LinkRouter style={{ color: 'rgba(0, 0, 0, 0.54)' }} className={classes.link} to="/">Наш Дом</LinkRouter>
                        {' '}
                        {new Date().getFullYear()}
                        {'.'}
                    </Typography>
                </Box>
            </Container>
        )
    }
}

export default withRouter(withStyles(styles)(Login))