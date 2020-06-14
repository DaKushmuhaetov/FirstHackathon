// React
import React, {Fragment} from 'react'

import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button';
// MaterialUI
import Grid from '@material-ui/core/Grid'

import VotingCard from '../Cards/Party'
import Http from '../../modules/http'
import {Context} from '../../context'


class Meetings extends React.PureComponent {
    static contextType = Context

    constructor(props) {
        super(props) 
 
        this.state =  {
            items: [
              ]
        }
    }
    componentDidMount() {
        this.onChange(undefined, 1)
    }

    onChange = async (e, page) => {
        let http = new Http(`/meetings`, 'GET', undefined, {'Authorization': `Bearer ${this.context.token}`})
        const response = await http.request().catch((status) => {
            console.log(status);
            return undefined
        })
        console.log(response.items)
        this.setState({items: response.items})


    }
    render() {
        return (
            <Fragment>                
            {!this.context.isAdmin() ? (
            <Grid container spacing={3}>            
                {this.state.items.map((value, index) => {
                        return(
                            <Grid key={index} item xs={12} sm={4}>
                                <VotingCard 
                                        title={value.title}
                                        date={value.meetingDate.substring(0, 10) + ' ' + value.meetingDate.substring(11)}
                                        text={value.description}
                                    />
                            </Grid>    
                        )
                    })}
            </Grid>
            ) : null}
            {this.context.isAdmin() ? (
                    <form className="el-center el-form" noValidate autoComplete="off">
                        <TextField style={{marginBottom: 20}} className="el-textField" id="outlined-basic" fullWidth label="Название мероприятия" variant="outlined" />
                        <TextField className="el-textField" id="outlined-multiline-flexible" multiline
                            rows={5} fullWidth label="описание мероприятия" variant="outlined" />
                        <Button style={{ marginTop: '25px' }} variant="contained" color="primary">Создать</Button>
                    </form>
                ) : null}    
            </Fragment>                
        )
    }
}

export default Meetings