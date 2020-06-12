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
import HomeIcon from '@material-ui/icons/Home';

// Context
import {Context} from '../../context'

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

class Registration extends React.PureComponent {
    static contextType = Context

    state = {
        email: '',
        password: ''
    }

    handleChange = (e) => {
        e.preventDefault()

        const val = e.target.value
        const name = e.target.name

        this.setState({ [name]: val })
    }

    handleRegister = (e) => {
        e.preventDefault()

        if (this.state.email === '' || this.state.password === '') {
            this.context.handleToast('Некорректный email или пароль', '#FF3333', 4000)
            return
        }

        console.log(this.state)
    }

    render() {
        const {classes} = this.props

        return (
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <div className={classes.paper}>
                    <Avatar className={classes.avatar}>
                        <HomeIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Регистрация
                    </Typography>
                    <form style={{ textAlign: 'center' }} className={classes.form} noValidate>
                        <TextField
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

                            onClick={this.handleRegister}
                        >
                            Регистрация
                        </Button>
                        <LinkRouter className={classes.link} to="/login">Есть аккаунт? Войдите</LinkRouter>
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

export default withRouter(withStyles(styles)(Registration))