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
import Grid from '@material-ui/core/Grid'
import DateFnsUtils from '@date-io/date-fns'
import {MuiPickersUtilsProvider, KeyboardDatePicker} from '@material-ui/pickers'

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
        marginTop: theme.spacing(4),
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
        firstName: '',
        lastName: '',
        date: new Date('2002-06-14T21:11:54'),
        email: '',
        password: '',

        invalidList: []
    }

    handleDateChange = (date) => {
        if (date.toString() === 'Invalid Date') {
            let newArr = this.state.invalidList.slice()
            newArr.push('date')

            this.setState({ invalidList: newArr })
        } else {
            if (this.state.invalidList.includes('date')) {
                let newArr = this.state.invalidList.slice()

                const index = newArr.indexOf('date')
                if (index > -1) {
                    newArr.splice(index, 1)
                    this.setState({ invalidList: newArr })
                }
            }
        }

        this.setState({ date })
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

        if (withoutSymbols.test(this.state.firstName) === false) {
            invalidList.push('firstName')
        }

        if (withoutSymbols.test(this.state.lastName) === false) {
            invalidList.push('lastName')
        }

        if (withoutSymbols.test(this.state.password) === false) {
            invalidList.push('password')
        }

        const email = /\S+@\S+\.\S+/
        if (email.test(this.state.email) === false) {
            invalidList.push('email')
        }

        if (this.state.invalidList.includes('date')) {
            invalidList.push('date')
        }

        this.setState({ invalidList }, () => console.log(this.state.invalidList))

        return invalidList.length > 0 ? invalidList : false
    }

    handleRegister = (e) => {
        e.preventDefault()

        if (this.handleValidate() !== false) return

        console.log(this.state)
    }

    render() {
        const {classes} = this.props
        const invalidList = this.state.invalidList

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
                        <Grid container spacing={2}>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    error={invalidList.includes('firstName')}

                                    autoComplete="fname"
                                    name="firstName"
                                    variant="outlined"
                                    required
                                    fullWidth
                                    id="firstName"
                                    label="Имя"
                                    autoFocus

                                    value={this.state.firstName}
                                    onChange={this.handleChange}
                                />
                            </Grid>
                            <Grid item xs={12} sm={6}>
                                <TextField
                                    error={invalidList.includes('lastName')}

                                    variant="outlined"
                                    required
                                    fullWidth
                                    id="lastName"
                                    label="Фамилия"
                                    name="lastName"
                                    autoComplete="lname"

                                    value={this.state.lastName}
                                    onChange={this.handleChange}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                    <KeyboardDatePicker
                                        fullWidth
                                        disableToolbar
                                        variant="inline"
                                        format="dd/MM/yyyy"
                                        id="date-picker-inline"
                                        label="Дата рождения"
                                        invalidDateMessage="Некорректная дата"
                                        KeyboardButtonProps={{
                                            'aria-label': 'change date',
                                        }}

                                        value={this.state.date}
                                        onChange={this.handleDateChange}
                                    />
                                </MuiPickersUtilsProvider>
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    error={invalidList.includes('email')}

                                    variant="outlined"
                                    required
                                    fullWidth
                                    id="email"
                                    label="Email адрес"
                                    name="email"
                                    autoComplete="email"

                                    value={this.state.email}
                                    onChange={this.handleChange}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    error={invalidList.includes('password')}

                                    variant="outlined"
                                    required
                                    fullWidth
                                    name="password"
                                    label="Пароль"
                                    type="password"
                                    id="password"
                                    autoComplete="current-password"

                                    value={this.state.password}
                                    onChange={this.handleChange}
                                />
                            </Grid>
                        </Grid>
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