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

// Context
import {Context} from '../../context'

// Modules
import Http from '../../modules/http'

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

class HouseRegistration extends React.PureComponent {
    static contextType = Context

    state = {
        address: '',
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
        
        if (this.state.address === '') {
            invalidList.push('address')
        }

        if (withoutSymbols.test(this.state.password) === false) {
            invalidList.push('password')
        }

        if (this.state.houseId === -1) {
            invalidList.push('house')
        }

        const email = /\S+@\S+\.\S+/
        if (email.test(this.state.email) === false) {
            invalidList.push('email')
        }

        this.setState({ invalidList }, () => console.log(this.state.invalidList))

        return invalidList.length > 0 ? invalidList : true
    }

    handleRegister = async (e) => {
        e.preventDefault()

        if (this.handleValidate() !== true) return

        const data = JSON.stringify({
            login: this.state.email,
            password: this.state.password
        })

        let http = new Http(`/houses/create?address=${this.state.address}`, 'POST', data, { 'Content-Type': 'application/json' })

        const response = await http.request().catch((status) => {
            
            switch (status) {
                case 400:
                    this.context.handleToast('Адрес должен быть вписан', '#DC143C', 5000)
                    break
                case 409:
                    this.context.handleToast('Дом с таким email уже зарегистрирован', '#DC143C', 5000)
                    break
                default:
                    break
            }

            return undefined
        })

        if (response === undefined) return

        this.context.login(response.token.access_token)
        document.location.reload(true)
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
                        Регистрация дома
                    </Typography>
                    <form style={{ textAlign: 'center' }} className={classes.form} noValidate>
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <TextField
                                    error={invalidList.includes('address')}

                                    variant="outlined"
                                    required
                                    fullWidth
                                    id="address"
                                    label="Адрес"
                                    name="address"
                                    autoComplete="lname"

                                    value={this.state.lastName}
                                    onChange={this.handleChange}
                                />
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

export default withRouter(withStyles(styles)(HouseRegistration))