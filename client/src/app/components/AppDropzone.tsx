import { UploadFile } from '@mui/icons-material';
import { FormControl, FormHelperText, Typography } from '@mui/material';
import { useCallback } from 'react'
import { useDropzone } from 'react-dropzone'
import { UseControllerProps, useController } from 'react-hook-form'

interface Props extends UseControllerProps {

}

function AppDropzone(props: Props) {
    const { fieldState, field } = useController({ ...props, defaultValue: null });

    const dzStyles = {
        display: 'flex',
        border: 'dashed 3px #eee',
        borderColor: '#eee',
        borderRadius: '5px',
        paddingTop: '30px',
        alignItems: 'center',
        height: 200,
        widh: 500
    }

    const dzActive = {
        borderColor: 'green'
    }

    const onDrop = useCallback((acceptedFiles:any) => {
        acceptedFiles[0] = Object.assign(acceptedFiles[0], 
            {preview: URL.createObjectURL(acceptedFiles[0])});
        field.onChange(acceptedFiles[0]);
    }, [field])
    const { getRootProps, getInputProps, isDragActive } = useDropzone({ onDrop })

    return (
        <div {...getRootProps()}>
            <FormControl style={isDragActive ? {...dzStyles, ...dzActive}: dzStyles} error={!!fieldState.error}>
                <input {...getInputProps()} />
                <UploadFile sx={{fontSize: '100px'}} />
                <Typography variant='h4'>Drop the image here</Typography>
                <FormHelperText>{fieldState.error?.message}</FormHelperText>
            </FormControl>
        </div>
    )
}

export default AppDropzone