(function () {
    "use strict"

    // for publish date picker
    flatpickr("#tran-date", {
        defaultDate: ["2023-04-17"]
    });
    flatpickr("#ship-date", {
        defaultDate: ["2023-04-17"]
    });

    flatpickr("#shipto-date", {
        defaultDate: ["2023-04-17"]
    });

    

    const form = document.getElementById('salesOrderForm');
        const editBtn = document.getElementById('editBtn');
        const saveBtn = document.getElementById('saveBtn');
        const cancelBtn = document.getElementById('cancelBtn');

        const choicesInstances = [];

        // Initialize Choices.js for all select elements in the form
        const selectElements = form.querySelectorAll('select');
        selectElements.forEach(select => {
            // if (select.classList.contains('choices__input')) {
            //     // Add to the list
            // }
            const choices = new Choices(select, {
                allowHTML: true,
                removeItemButton: select.multiple?true:false,
              });
              

              select.addEventListener(
                'showDropdown',
                function(event) {
                    var closestDiv = select.closest('div.table-responsive');

                    if (closestDiv) {
                      // Set the CSS property 'overflow' to 'visible'
                      closestDiv.style.overflow = 'visible';
                      //closestDiv.style.overflowX = 'hidden !important';
                    }
                },
                false,
              );

              select.addEventListener(
                'hideDropdown',
                function(event) {
                    var closestDiv = select.closest('div.table-responsive');

                    if (closestDiv) {
                      // Set the CSS property 'overflow' to 'visible'
                      closestDiv.style.overflow = 'auto';
                      //closestDiv.style.overflowY = 'auto';
                    }
                },
                false,
              );
                choicesInstances.push(choices); 
        });

        const table = document.getElementById('order-lines-table');
        const rows = table.rows;
        const tfoot = document.querySelector('#order-lines-table tfoot');

        // Function to set the read-only state of the form
        function setFormReadonly(isReadonly) {
            const elements = form.elements;

            
            choicesInstances.forEach(choices => {
                if (isReadonly) {
                    choices.disable(); // Disable all Choices instances
                } else {
                    choices.enable(); // Enable all Choices instances
                    console.log(choices)
                }
            });

            for (let i = 0; i < elements.length; i++) {
                const element = elements[i];
                if (element.tagName === 'BUTTON' && element.classList.contains('to-hide')) element.style.display =isReadonly? 'none':'inline-block'; // Skip buttons
                else 
                if (element.type === 'radio' || element.type === 'checkbox' || element.tagName === 'SELECT') {
                    element.disabled = isReadonly;
                }
                 else if(element.tagName !== 'BUTTON') {
                    element.disabled = isReadonly;
                }
            }

            // choicesInstances.forEach(choices => {
            //     if (isReadonly) {
            //         choices.disable(); // Disable all Choices instances
            //     } else {
            //         choices.enable(); // Enable all Choices instances
            //         choices.refresh();
            //     }
            // });
             // Toggle button visibility based on read-only state
             if (isReadonly) {
                editBtn.style.display = 'inline-block';
                saveBtn.style.display = 'none';
                cancelBtn.style.display = 'none';
                tfoot.style.display = 'none';
                for (let i = 0; i < rows.length; i++) {
                    rows[i].cells[0].style.display = 'none';
                }
            } else {
                editBtn.style.display = 'none';
                saveBtn.style.display = 'inline-block';
                cancelBtn.style.display = 'inline-block';
                tfoot.style.display = 'table-footer-group';
                for (let i = 0; i < rows.length; i++) {
                    rows[i].cells[0].style.display = 'table-cell';
                }
            }
        }

        // Initialize form as readonly
        setFormReadonly(true);

        // Add event listeners to buttons
        editBtn.addEventListener('click', () => setFormReadonly(false));
        //saveBtn.addEventListener('click', () => setFormReadonly(true));
        cancelBtn.addEventListener('click', () => setFormReadonly(true));

        document.addEventListener('keydown', (event) => {
            const activeElement = document.activeElement;
        
            // Check if the active element is inside the footer row
            if (activeElement && activeElement.closest('tfoot')) {
                // Check if the pressed key is "Enter"
                if (event.key === 'Enter') {
                    event.preventDefault(); // Prevent default Enter behavior
        
                    // Find the "+" button in the footer row
                    const footer = activeElement.closest('tfoot');
                    const addButton = footer.querySelector('.add-row'); // Assumes the "+" button has class "add-row"
        
                    if (addButton) {
                        addButton.click(); // Trigger the click event on the "+" button
                    }
                }
            }
        });

        
        const addRowBtn = document.getElementById('add-row');
        addRowBtn.addEventListener('click', (e)=>{
            alert('adding new ror.');            
        });
})();