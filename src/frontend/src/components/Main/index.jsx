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
                            Инновационный сервис для обеспечения управления многоквартирным домом, а также повышения коммуникации между жильцами и выполнения их нужд.
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