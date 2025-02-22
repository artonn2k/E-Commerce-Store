import { FormGroup, FormControlLabel, Checkbox } from '@mui/material';
import { useState } from 'react';

interface Props {
    items: string[];
    checked?: string[];
    onChange: (items: string[]) => void;
}

//function when whe click one of the filters we are going to get a new array
// were going to use that array to pass to our redux state, and that triggers to get the new product list
function CheckboxButtons({items, checked, onChange}: Props) {
  const [checkedItems, setCheckedItems] = useState(checked || []);

  function handleChecked(value: string) {
    const currentIndex = checkedItems.findIndex(item => item === value);
    let newChecked: string[] = [];
    if(currentIndex ===  -1) newChecked = [...checkedItems, value];
    else newChecked = checkedItems.filter(item => item !== value);
    setCheckedItems(newChecked);
    onChange(newChecked);
  }

  return (
    <FormGroup>
        {items.map(item => (
            <FormControlLabel 
              control={<Checkbox 
                checked={checkedItems.indexOf(item) !== -1}
                onClick={() => handleChecked(item)}
              />} 
              label={item} 
              key={item}
            />
        ))}
    </FormGroup>
  )
}

export default CheckboxButtons