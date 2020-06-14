// React
import React, {Fragment} from 'react'

import InfoCard from '../Cards/Info'
import Image from '../../images/gopniki.jpg'
// MaterialUI
import Grid from '@material-ui/core/Grid'
import Pagination from '@material-ui/lab/Pagination';

import './index.css'
import {Context} from '../../context'

import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button';

import Http from '../../modules/http'

class News extends React.PureComponent {
    static contextType = Context

    constructor(props) {
        super(props) 
        
        this.state =  {
            total: 1,
            items: [
              ]
        }
    }

    parseImage = (data) => {
        if(data != undefined)
            return `data:image/jpeg;base64,${data}`
        else
            return Image
        

    }

    componentDidMount() {
        this.onChange(undefined, 1)
    }

    onChange = async (e, page) => {
        let http = new Http(`/house/news`, 'GET', null, {'Authorization': `Bearer ${this.context.token}`})

        const response = await http.request().catch((status) => {
            console.log(status);
            return undefined
        })

        console.log(response.items)
        this.setState({items: response.items})
        this.setState({total: response.total/3})
    }

    render() {
        return (
            <Fragment>                
                {!this.context.isAdmin() ? (
                <div>
                    <Grid container spacing={3}>   
                    {this.state.items.map((value, index) => {
                        return(
                            <Grid key={index} item xs={12} sm={4}>
                                <InfoCard 
                                        title={value.title}
                                        date={value.createDate.substring(0, 10) + ' ' + value.createDate.substring(11)}
                                        image={this.parseImage(value.image)}
                                        text={value.description}
                                    />
                            </Grid>    
                        )
                    })}
                    </Grid>
                    <Pagination className='pogination' count={this.state.total} color="primary" onChange={this.onChange}/>
                </div>
                
                ): null}
                {this.context.isAdmin() ? (
                    <form className="el-center el-form" noValidate autoComplete="off">
                        <TextField style={{marginBottom: 20}} className="el-textField" id="outlined-basic" fullWidth label="Название новости" variant="outlined" />
                        <TextField className="el-textField" id="outlined-multiline-flexible" multiline
                            rows={5} fullWidth label="описание новости" variant="outlined" />
                        <Button style={{ marginTop: '25px' }} variant="contained" color="primary">Создать</Button>
                    </form>
                ) : null}
            </Fragment>
            
        )
    }
}

export default News