// React
import React from 'react'

// Router
import {withRouter} from 'react-router-dom'
import logo from '../../images/logo.svg'
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';

// Context
import {Context} from '../../context'

// Styles
import './index.css'

class Main extends React.PureComponent {
    static contextType = Context

    render() {
        return (    
            <div className="main">
                
                <div className="main-inner">
                    <img className="main-logo" src={logo} alt=""/>
                    <div className="main-center">

                        <Typography variant="h3" gutterBottom className="main-title">
                            Наш Дом
                        </Typography>

                        <Typography variant="h5" gutterBottom className="main-description">
                            Lorem ipsum dolor, sit amet consectetur adipisicing elit. Iusto nisi deleniti quod quos adipisci, animi dolore vitae quidem dolorem harum molestias sint eligendi obcaecati fugiat provident porro, vel consectetur minima?
                        </Typography>

                        <Button
                            onClick={() => this.props.history.push('/registration')}
                            variant="contained"
                            color="primary"
                            className="main-btn"
                        >
                            Начать
                        </Button>

                    </div>
                </div>
            </div>
            
        )
    }
}

export default withRouter(Main)