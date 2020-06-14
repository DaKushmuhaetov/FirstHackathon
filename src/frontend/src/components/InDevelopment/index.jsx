// React
import React from 'react'
import Typography from '@material-ui/core/Typography';

class InDevelopment extends React.PureComponent {

    render() {
        return (
            <Typography className='tab-h4' variant="h4" component="h2">
                Раздел находится в разработке...
            </Typography>
        )
    }
}

export default InDevelopment