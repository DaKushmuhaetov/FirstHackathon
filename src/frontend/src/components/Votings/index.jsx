// React
import React, {Fragment} from 'react'

// MaterialUI
import Grid from '@material-ui/core/Grid'

import Image from '../../images/gopniki.jpg'

import Http from '../../modules/http'
import VotingCard from '../Cards/Voting/'

import {Context} from '../../context'

import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button';

import './index.css'

class Votings extends React.PureComponent {
    static contextType = Context

    constructor(props) {
        super(props) 
        this.state =  {
            items: [
              ],
            aVariantTotal: 1,
            aVariantList: []
        }
    }
    componentDidMount() {
        this.onChange(undefined, 1)
    }
    onChange = async (e, page) => {
        let http
        if(this.context.isAdmin()){
            http = new Http(`/house/votings`, 'GET', undefined, {'Authorization': `Bearer ${this.context.token}`})
        } else {
            http = new Http(`/votings`, 'GET', undefined, {'Authorization': `Bearer ${this.context.token}`})
        }
        const response = await http.request().catch((status) => {
            console.log(status);
            return undefined
        })
        
        // Рамиль ******, говорил в процентах отправляй, ****, нет, *****, число вернул
 
        this.setState({items: response.items})
    }
    addVariant = () => {
        this.state.aVariantList.push(`ref${this.state.aVariantTotal+1}`)
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
                                                votingID={value.id}
                                                title={value.title}
                                                date="14.06.2020"
                                                image={Image}
                                                variants={value.variants}
                                            />
                                    </Grid>    
                                )
                            })}
                    </Grid>
                ) : null}

                {this.context.isAdmin() ? (
                    <form className="el-center el-form" noValidate autoComplete="off">
                        <TextField className="el-textField" id="outlined-basic" fullWidth label="Название голосования" variant="outlined" />
                        {this.state.aVariantList.map((value, index) => {
                            return (
                                <TextField ref={value} className="el-textField" id="outlined-basic" fullWidth label={`Вариант ${index}`} variant="outlined" />
                            )
                        })}
                        <Button style={{ marginTop: '25px' }} variant="contained">Добавить вариант</Button>
                        <Button style={{ marginTop: '25px' }} variant="contained" color="primary">Создать</Button>
                    </form>
                ) : null}
            </Fragment>
        )
    }
}

export default Votings