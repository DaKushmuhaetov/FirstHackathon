// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'

// MaterialUI
import ListItem from '@material-ui/core/ListItem'
import ListItemIcon from '@material-ui/core/ListItemIcon'
import ListItemText from '@material-ui/core/ListItemText'
import PeopleIcon from '@material-ui/icons/People'
import BarChartIcon from '@material-ui/icons/BarChart'
import Typography from '@material-ui/core/Typography'
import { withStyles } from '@material-ui/core/styles'
import HomeIcon from '@material-ui/icons/Home'

import clsx from 'clsx';
import CssBaseline from '@material-ui/core/CssBaseline'
import Drawer from '@material-ui/core/Drawer'
import AppBar from '@material-ui/core/AppBar'
import Toolbar from '@material-ui/core/Toolbar'
import List from '@material-ui/core/List'
import Divider from '@material-ui/core/Divider'
import IconButton from '@material-ui/core/IconButton'
import Badge from '@material-ui/core/Badge'
import Container from '@material-ui/core/Container'
import Grid from '@material-ui/core/Grid'
import MenuIcon from '@material-ui/icons/Menu'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft'
import NotificationsIcon from '@material-ui/icons/Notifications'

import AnnouncementIcon from '@material-ui/icons/Announcement';
import TelegramIcon from '@material-ui/icons/Telegram';

// Styles
import './index.css'

class UserPanel extends React.PureComponent {
    state = {
        open: true
    }

    handleDrawerOpen = () => {
        this.setState({ open: true })
    }

    handleDrawerClose = () => {
        this.setState({ open: false })
    }

    render() {
        const {classes} = this.props
        const open = this.state.open

        return (
            <div className={classes.root}>
                <CssBaseline />
                <AppBar position="absolute" className={clsx(classes.appBar, open && classes.appBarShift)}>
                    <Toolbar className={classes.toolbar}>
                    <IconButton
                        edge="start"
                        color="inherit"
                        aria-label="open drawer"
                        onClick={this.handleDrawerOpen}
                        className={clsx(classes.menuButton, open && classes.menuButtonHidden)}
                    >
                        <MenuIcon />
                    </IconButton>
                    <HomeIcon className={classes.homeIcon}/>
                    <Typography component="h1" variant="h6" color="inherit" noWrap className={classes.title}>
                        Наш Дом - г. Москва, ул. Ленина, д. 228
                    </Typography>
                    <IconButton color="inherit">
                        <Badge badgeContent={0} color="secondary">
                        <NotificationsIcon />
                        </Badge>
                    </IconButton>
                    </Toolbar>
                </AppBar>
                <Drawer
                    variant="permanent"
                    classes={{
                    paper: clsx(classes.drawerPaper, !open && classes.drawerPaperClose),
                    }}
                    open={open}
                >
                    <div className={classes.toolbarIcon}>
                    <IconButton onClick={this.handleDrawerClose}>
                        <ChevronLeftIcon />
                    </IconButton>
                    </div>
                    <Divider />
                    <List>{LeftList}</List>
                </Drawer>
                <main className={classes.content}>
                    <div className={classes.appBarSpacer} />
                    <Container maxWidth="lg" className={classes.container}>
                        <Grid container spacing={3}>
                            
                        </Grid>
                    </Container>
                </main>
            </div>
        )
    }
}

const LeftList = (
    <div>
        <ListItem button>
            <ListItemIcon>
                <AnnouncementIcon />
            </ListItemIcon>
            <ListItemText primary="Новости" />
            </ListItem>
            <ListItem button>
            <ListItemIcon>
                <TelegramIcon />
            </ListItemIcon>
            <ListItemText primary="Общий чат" />
            </ListItem>
            <ListItem button>
            <ListItemIcon>
                <PeopleIcon />
            </ListItemIcon>
            <ListItemText primary="Собрания" />
            </ListItem>
            <ListItem button>
            <ListItemIcon>
                <BarChartIcon />
            </ListItemIcon>
            <ListItemText primary="Голосования" />
            </ListItem>
            <ListItem button>
        </ListItem>
    </div>
)

const drawerWidth = 240;

const styles = theme => ({
    root: {
        display: 'flex',

        position: 'fixed',
        top: 0,
        left: 0,

        width: '100%',
        height: '100vh',

        backgroundColor: '#fff'
    },
    homeIcon: {
        marginRight: '10px'
    },
    toolbar: {
        paddingRight: 24,
    },
    toolbarIcon: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'flex-end',
        padding: '0 8px',
        ...theme.mixins.toolbar,
    },
    appBar: {
        zIndex: theme.zIndex.drawer + 1,
        transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
        }),
    },
    appBarShift: {
        marginLeft: drawerWidth,
        width: `calc(100% - ${drawerWidth}px)`,
        transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
        }),
    },
    menuButton: {
        marginRight: 36,
    },
    menuButtonHidden: {
        display: 'none',
    },
    title: {
        flexGrow: 1,
    },
    drawerPaper: {
        position: 'relative',
        whiteSpace: 'nowrap',
        width: drawerWidth,
        transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
        }),
    },
    drawerPaperClose: {
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(7),
        [theme.breakpoints.up('sm')]: {
        width: theme.spacing(9),
        },
    },
    appBarSpacer: theme.mixins.toolbar,
    content: {
        flexGrow: 1,
        height: '100vh',
        overflow: 'auto',
    },
    container: {
        paddingTop: theme.spacing(4),
        paddingBottom: theme.spacing(4),
    },
    paper: {
        padding: theme.spacing(2),
        display: 'flex',
        overflow: 'auto',
        flexDirection: 'column',
    },
    fixedHeight: {
        height: 240,
    },
})

export default withRouter(withStyles(styles)(UserPanel))