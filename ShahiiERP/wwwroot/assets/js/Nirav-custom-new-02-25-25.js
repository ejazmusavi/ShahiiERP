(function () {
  "use strict";

  /* page loader */
  function hideLoader() {
    const loader = document.getElementById("loader");
    loader.classList.add("d-none")
  }
  window.addEventListener("load", hideLoader);
  /* page loader */

  /* tooltip */
  const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
  const tooltipList = [...tooltipTriggerList].map(
    (tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl)
  );

  /* popover  */
  const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
  const popoverList = [...popoverTriggerList].map(
    (popoverTriggerEl) => new bootstrap.Popover(popoverTriggerEl)
  );

  //switcher color pickers
  const pickrContainerPrimary = document.querySelector(".pickr-container-primary");
  const themeContainerPrimary = document.querySelector(".theme-container-primary");
  const pickrContainerBackground = document.querySelector(".pickr-container-background");
  const themeContainerBackground = document.querySelector(".theme-container-background");

  /* for theme primary */
  const nanoThemes = [["nano", {
        defaultRepresentation: "RGB",
        components: {
          preview: true,
          opacity: false,
          hue: true,

          interaction: {
            hex: false,
            rgba: true,
            hsva: false,
            input: true,
            clear: false,
            save: false,
          },
        },
      },
    ],
  ];
  const nanoButtons = [];
  let nanoPickr = null;
  for (const [theme, config] of nanoThemes) {
    const button = document.createElement("button");
    button.innerHTML = theme;
    nanoButtons.push(button);

    button.addEventListener("click", () => {
      const el = document.createElement("p");
      pickrContainerPrimary.appendChild(el);

      /* Delete previous instance */
      if (nanoPickr) {
        nanoPickr.destroyAndRemove();
      }

      /* Apply active class */
      for (const btn of nanoButtons) {
        btn.classList[btn === button ? "add" : "remove"]("active");
      }

      /* Create fresh instance */
      nanoPickr = new Pickr(
        Object.assign(
          {
            el,
            theme,
            default: "#845adf",
          },
          config
        )
      );

      /* Set events */
      nanoPickr.on("changestop", (source, instance) => {
        let color = instance.getColor().toRGBA();
        let html = document.querySelector("html");
        html.style.setProperty(
          "--primary-rgb",
          `${Math.floor(color[0])}, ${Math.floor(color[1])}, ${Math.floor(
            color[2]
          )}`
        );
        /* theme color picker */
        localStorage.setItem(
          "primaryRGB",
          `${Math.floor(color[0])}, ${Math.floor(color[1])}, ${Math.floor(
            color[2]
          )}`
        );
        updateColors();
      });
    });

    themeContainerPrimary.appendChild(button);
  }
  nanoButtons[0].click();
  /* for theme primary */

  /* for theme background */
  const nanoThemes1 = [
    [
      "nano",
      {
        defaultRepresentation: "RGB",
        components: {
          preview: true,
          opacity: false,
          hue: true,

          interaction: {
            hex: false,
            rgba: true,
            hsva: false,
            input: true,
            clear: false,
            save: false,
          },
        },
      },
    ],
  ];
  const nanoButtons1 = [];
  let nanoPickr1 = null;
  for (const [theme, config] of nanoThemes) {
    const button = document.createElement("button");
    button.innerHTML = theme;
    nanoButtons1.push(button);

    button.addEventListener("click", () => {
      const el = document.createElement("p");
      pickrContainerBackground.appendChild(el);

      /* Delete previous instance */
      if (nanoPickr1) {
        nanoPickr1.destroyAndRemove();
      }

      /* Apply active class */
      for (const btn of nanoButtons) {
        btn.classList[btn === button ? "add" : "remove"]("active");
      }

      /* Create fresh instance */
      nanoPickr1 = new Pickr(
        Object.assign(
          {
            el,
            theme,
            default: "#845adf",
          },
          config
        )
      );

      /* Set events */
      nanoPickr1.on("changestop", (source, instance) => {
        let color = instance.getColor().toRGBA();
        let html = document.querySelector("html");
        html.style.setProperty(
          "--body-bg-rgb",
          `${color[0]}, ${color[1]}, ${color[2]}`
        );
        document
          .querySelector("html")
          .style.setProperty(
            "--body-bg-rgb2",
            `${color[0] + 14}, ${color[1] + 14}, ${color[2] + 14}`
          );
        document
          .querySelector("html")
          .style.setProperty(
            "--light-rgb",
            `${color[0] + 14}, ${color[1] + 14}, ${color[2] + 14}`
          );
        document
          .querySelector("html")
          .style.setProperty(
            "--form-control-bg",
            `rgb(${color[0] + 14}, ${color[1] + 14}, ${color[2] + 14})`
          );
        localStorage.removeItem("bgtheme");
        updateColors();
        html.setAttribute("data-theme-mode", "dark");
        html.setAttribute("data-menu-styles", "dark");
        html.setAttribute("data-header-styles", "dark");
        document.querySelector("#switcher-dark-theme").checked = true;
        localStorage.setItem(
          "bodyBgRGB",
          `${color[0]}, ${color[1]}, ${color[2]}`
        );
        localStorage.setItem(
          "bodylightRGB",
          `${color[0] + 14}, ${color[1] + 14}, ${color[2] + 14}`
        );
      });
    });
    themeContainerBackground.appendChild(button);
  }
  nanoButtons1[0].click();
  /* for theme background */

  /* header theme toggle */
  function toggleTheme() {
    let html = document.querySelector("html");
    if (html.getAttribute("data-theme-mode") === "dark") {
      html.setAttribute("data-theme-mode", "light");
      html.setAttribute("data-header-styles", "light");
      html.setAttribute("data-menu-styles", "light");
      if (!localStorage.getItem("primaryRGB")) {
        html.setAttribute("style", "");
      }
      html.removeAttribute("data-bg-theme");
      document.querySelector("#switcher-light-theme").checked = true;
      document.querySelector("#switcher-menu-light").checked = true;
      document
        .querySelector("html")
        .style.removeProperty("--body-bg-rgb", localStorage.bodyBgRGB);
      checkOptions();
      html.style.removeProperty("--body-bg-rgb2");
      html.style.removeProperty("--light-rgb");
      html.style.removeProperty("--form-control-bg");
      html.style.removeProperty("--input-border");
      document.querySelector("#switcher-header-light").checked = true;
      document.querySelector("#switcher-menu-light").checked = true;
      document.querySelector("#switcher-light-theme").checked = true;
      document.querySelector("#switcher-background4").checked = false;
      document.querySelector("#switcher-background3").checked = false;
      document.querySelector("#switcher-background2").checked = false;
      document.querySelector("#switcher-background1").checked = false;
      document.querySelector("#switcher-background").checked = false;
      localStorage.removeItem("ynexdarktheme");
      localStorage.removeItem("ynexMenu");
      localStorage.removeItem("ynexHeader");
      localStorage.removeItem("bodylightRGB");
      localStorage.removeItem("bodyBgRGB");
      if (localStorage.getItem("ynexlayout") != "horizontal") {
        html.setAttribute("data-menu-styles", "dark");
      }
      html.setAttribute("data-header-styles", "light");
    } else {
      html.setAttribute("data-theme-mode", "dark");
      html.setAttribute("data-header-styles", "dark");
      if (!localStorage.getItem("primaryRGB")) {
        html.setAttribute("style", "");
      }
      html.setAttribute("data-menu-styles", "dark");
      document.querySelector("#switcher-dark-theme").checked = true;
      document.querySelector("#switcher-menu-dark").checked = true;
      document.querySelector("#switcher-header-dark").checked = true;
      checkOptions();
      document.querySelector("#switcher-menu-dark").checked = true;
      document.querySelector("#switcher-header-dark").checked = true;
      document.querySelector("#switcher-dark-theme").checked = true;
      document.querySelector("#switcher-background4").checked = false;
      document.querySelector("#switcher-background3").checked = false;
      document.querySelector("#switcher-background2").checked = false;
      document.querySelector("#switcher-background1").checked = false;
      document.querySelector("#switcher-background").checked = false;
      localStorage.setItem("ynexdarktheme", "true");
      localStorage.setItem("ynexMenu", "dark");
      localStorage.setItem("ynexHeader", "dark");
      localStorage.removeItem("bodylightRGB");
      localStorage.removeItem("bodyBgRGB");
    }
  }
  let layoutSetting = document.querySelector(".layout-setting");
  layoutSetting.addEventListener("click", toggleTheme);
  /* header theme toggle */

  /* Choices JS */
  document.addEventListener("DOMContentLoaded", function () {
    var genericExamples = document.querySelectorAll("[data-trigger]");
    for (let i = 0; i < genericExamples.length; ++i) {
      var element = genericExamples[i];
      new Choices(element, {
        allowHTML: true,
        placeholderValue: "This is a placeholder set in the config",
        searchPlaceholderValue: "Search",
      });
    }
  });
  /* Choices JS */

  /* footer year */
  document.getElementById("year").innerHTML = new Date().getFullYear();
  /* footer year */

  /* node waves */
  Waves.attach(".btn-wave", ["waves-light"]);
  Waves.init();
  /* node waves */

  /* card with close button */
  let DIV_CARD = ".card";
  let cardRemoveBtn = document.querySelectorAll(
    '[data-bs-toggle="card-remove"]'
  );
  cardRemoveBtn.forEach((ele) => {
    ele.addEventListener("click", function (e) {
      e.preventDefault();
      let $this = this;
      let card = $this.closest(DIV_CARD);
      card.remove();
      return false;
    });
  });
  /* card with close button */

  /* card with fullscreen */
  let cardFullscreenBtn = document.querySelectorAll(
    '[data-bs-toggle="card-fullscreen"]'
  );
  cardFullscreenBtn.forEach((ele) => {
    ele.addEventListener("click", function (e) {
      let $this = this;
      let card = $this.closest(DIV_CARD);
      card.classList.toggle("card-fullscreen");
      card.classList.remove("card-collapsed");
      e.preventDefault();
      return false;
    });
  });
  /* card with fullscreen */

  /* count-up */
  var i = 1;
  setInterval(() => {
    document.querySelectorAll(".count-up").forEach((ele) => {
      if (ele.getAttribute("data-count") >= i) {
        i = i + 1;
        ele.innerText = i;
      }
    });
  }, 10);
  /* count-up */

  /* back to top */
  const scrollToTop = document.querySelector(".scrollToTop");
  const $rootElement = document.documentElement;
  const $body = document.body;
  window.onscroll = () => {
    const scrollTop = window.scrollY || window.pageYOffset;
    const clientHt = $rootElement.scrollHeight - $rootElement.clientHeight;
    if (window.scrollY > 100) {
      scrollToTop.style.display = "flex";
    } else {
      scrollToTop.style.display = "none";
    }
  };
  scrollToTop.onclick = () => {
    window.scrollTo(0, 0);
  };
  /* back to top */

  /* header dropdowns scroll */
  var myHeaderShortcut = document.getElementById("header-shortcut-scroll");
  new SimpleBar(myHeaderShortcut, { autoHide: true });

  var myHeadernotification = document.getElementById(
    "header-notification-scroll"
  );
  new SimpleBar(myHeadernotification, { autoHide: true });

  var myHeaderCart = document.getElementById("header-cart-items-scroll");
  new SimpleBar(myHeaderCart, { autoHide: true });
  /* header dropdowns scroll */
})();

/* full screen */
var elem = document.documentElement;
function openFullscreen() {
  let open = document.querySelector(".full-screen-open");
  let close = document.querySelector(".full-screen-close");

  if (
    !document.fullscreenElement &&
    !document.webkitFullscreenElement &&
    !document.msFullscreenElement
  ) {
    if (elem.requestFullscreen) {
      elem.requestFullscreen();
    } else if (elem.webkitRequestFullscreen) {
      /* Safari */
      elem.webkitRequestFullscreen();
    } else if (elem.msRequestFullscreen) {
      /* IE11 */
      elem.msRequestFullscreen();
    }
    close.classList.add("d-block");
    close.classList.remove("d-none");
    open.classList.add("d-none");
  } else {
    if (document.exitFullscreen) {
      document.exitFullscreen();
    } else if (document.webkitExitFullscreen) {
      /* Safari */
      document.webkitExitFullscreen();
      console.log("working");
    } else if (document.msExitFullscreen) {
      /* IE11 */
      document.msExitFullscreen();
    }
    close.classList.remove("d-block");
    open.classList.remove("d-none");
    close.classList.add("d-none");
    open.classList.add("d-block");
  }
}
/* full screen */

/* toggle switches */
let customSwitch = document.querySelectorAll(".toggle");
customSwitch.forEach((e) =>
  e.addEventListener("click", () => {
    e.classList.toggle("on");
  })
);
/* toggle switches */

/* header dropdown close button */

/* for cart dropdown */
const headerbtn = document.querySelectorAll(".dropdown-item-close");
headerbtn.forEach((button) => {
  button.addEventListener("click", (e) => {
    e.preventDefault();
    e.stopPropagation();
    button.parentNode.parentNode.parentNode.parentNode.parentNode.remove();
    document.getElementById("cart-data").innerText = `${
      document.querySelectorAll(".dropdown-item-close").length
    } Items`;
    document.getElementById("cart-icon-badge").innerText = `${
      document.querySelectorAll(".dropdown-item-close").length
    }`;
    console.log(
      document.getElementById("header-cart-items-scroll").children.length
    );
    if (document.querySelectorAll(".dropdown-item-close").length == 0) {
      let elementHide = document.querySelector(".empty-header-item");
      let elementShow = document.querySelector(".empty-item");
      elementHide.classList.add("d-none");
      elementShow.classList.remove("d-none");
    }
  });
});
/* for cart dropdown */

/* for notifications dropdown */
const headerbtn1 = document.querySelectorAll(".dropdown-item-close1");
headerbtn1.forEach((button) => {
  button.addEventListener("click", (e) => {
    e.preventDefault();
    e.stopPropagation();
    button.parentNode.parentNode.parentNode.parentNode.remove();
    document.getElementById("notifiation-data").innerText = `${
      document.querySelectorAll(".dropdown-item-close1").length
    } Unread`;
    document.getElementById("notification-icon-badge").innerText = `${
      document.querySelectorAll(".dropdown-item-close1").length
    }`;
    if (document.querySelectorAll(".dropdown-item-close1").length == 0) {
      let elementHide1 = document.querySelector(".empty-header-item1");
      let elementShow1 = document.querySelector(".empty-item1");
      elementHide1.classList.add("d-none");
      elementShow1.classList.remove("d-none");
    }
  });
});
/* for notifications dropdown */


const values = [
  {
    value: "1",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/us_flag.jpg" alt=""> <span class="mx-1">United States</span>',
    id: 1,
  },
  {
    value: "2",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/spain_flag.jpg"  alt=""> <span class="ms-1">Spain</span>',
    id: 2,
  },
  {
    value: "3",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/french_flag.jpg" alt=""> <span class="ms-1">France</span>',
    id: 3,
  },
  {
    value: "4",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/germany_flag.jpg" alt=""> <span class="ms-1">Germany</span>',
    id: 4,
  },
  {
    value: "5",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/italy_flag.jpg" alt=""> <span class="ms-1">Italy</span>',
    id: 5,
  },
  {
    value: "6",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/russia_flag.jpg" alt=""> <span class="ms-1">Netherlands</span>',
    id: 6,
  },
  {
    value: "7",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/argentina_flag.jpg" alt=""> <span class="ms-1">Argentina</span>',
    id: 7,
  },
  {
    value: "8",
    label:
      '<img class="avatar avatar-xs avatar-rounded" src="../assets/images/flags/argentina_flag.jpg" alt=""> <span class="ms-1">Argentina</span>',
    id: 8,
  },
];
const elements = document.querySelectorAll(".choices-images");
elements.forEach(function (element) {
  const choices = new Choices(element, {
    choices: values,
  });
});

const paginationDropdownItems = document.querySelectorAll('.pagination-dropdown .dropdown-item');
paginationDropdownItems.forEach(item => {
    item.addEventListener('click', function(e) {
        e.preventDefault();
        const perPage = this.textContent;

        item.closest('.pagination-dropdown').querySelectorAll('.dropdown-item').forEach(el => el.classList.remove('page-records'));
        item.classList.add('page-records');
        
        // Update button text
        const dropdownButton = this.closest('.btn-group').querySelector('.dropdown-toggle');
        dropdownButton.textContent = perPage + " Per Page";
    });
});

const actionDropdownItems = document.querySelectorAll('.action-dropdown .dropdown-item');
actionDropdownItems.forEach(item => {
    item.addEventListener('click', function(e) {
        e.preventDefault();
        const action = this.textContent;
        
        // Update button text
        const dropdownButton = this.closest('.btn-group').querySelector('.dropdown-toggle');
        dropdownButton.textContent = action;
    });
});

// Add event listeners to all delete buttons 
document.querySelectorAll('.ri-delete-bin-line').forEach(deleteBtn => { 
  deleteBtn.closest('a').addEventListener('click', function(e) { 
    e.preventDefault(); // Prevent default link behavior
    Swal.fire({ 
      title: 'Are you sure?', 
      text: "You won't be able to revert this!", 
      icon: 'warning', 
      showCancelButton: true, 
      confirmButtonColor: '#3085d6', 
      cancelButtonColor: '#d33', 
      confirmButtonText: 'Yes, delete it!' 
    })
      .then((result) => { 
      if (result.isConfirmed) { 
        // Here you can add your delete logic 
        Swal.fire( 'Deleted!', 'Your record has been deleted.', 'success' ) 
      } 
    }); 
  }); 
});

let checkedCount;
document.addEventListener("DOMContentLoaded", function () {
  const checkboxes = document.querySelectorAll("#myTable td .form-check-md .form-check-input");
  const selectAll = document.querySelector("#myTable th .form-check-md .form-check-input");
  const countDisplay = document.getElementsByClassName("link-danger"); // Display count (optional)

  function updateCheckedCount() {
      checkedCount = document.querySelectorAll(".checked-class").length;
      countDisplay[0].innerText = `Delete(${checkedCount})`;
      
      const dropdownButton = $('.link-danger').closest('.btn-group').find('.dropdown-toggle');
      if(dropdownButton[0].innerText.trim().startsWith("Delete")) {
          dropdownButton[0].innerText = `Delete(${checkedCount})`;
      }
  }

  selectAll.addEventListener("change", function () {
      checkboxes.forEach(checkbox => {
          checkbox.checked = this.checked;
          if (this.checked) {
              checkbox.classList.add("checked-class"); // Add class when checked
          } else {
              checkbox.classList.remove("checked-class"); // Remove class when unchecked
          }
      });
      updateCheckedCount(); // Update count
  });

  checkboxes.forEach(checkbox => {
      checkbox.addEventListener("change", function () {
          if (this.checked) {
              this.classList.add("checked-class"); // Add class when checked
          } else {
              this.classList.remove("checked-class"); // Remove class when unchecked
          }
          updateCheckedCount(); // Update count
      });
  });
});

  // const dltBtn = document.getElementsByClassName("link-danger");
$(".link-danger").closest('a').on('click', function(e) { 
  e.preventDefault(); // Prevent default link behavior
  const isChecked = checkedCount !== undefined && checkedCount !== 0;
  Swal.fire({ 
    title: isChecked ? `Are you sure to delete these ${checkedCount} records?` : 'Please Select any one record to delete', 
    text: isChecked && "You won't be able to revert this!", 
    icon: 'warning', 
    showCancelButton: isChecked ? true : false, 
    confirmButtonColor: '#3085d6', 
    cancelButtonColor: '#d33', 
    confirmButtonText: isChecked ? 'Yes, delete it!' : 'OK'
  })
    .then((result) => { 
    if (result.isConfirmed && isChecked) { 
      // Here you can add your delete logic 
      Swal.fire( 'Deleted!', 'Your record has been deleted.', 'success' ) 
    } 
  }); 
}); 

document.addEventListener("DOMContentLoaded", function () {

  // Get the header checkbox using its ID
  const headerCheckbox = document.querySelector('#checkebox-md');
  
  // Get all checkboxes inside the table body (excluding the header checkbox)
  const rowCheckboxes = document.querySelectorAll('tbody input[type="checkbox"]');
  
  // Function to update the header checkbox state and styling
  function updateHeaderCheckboxState() {
    const checkedCheckboxes = Array.from(rowCheckboxes).filter(checkbox => checkbox.checked);
    const totalCheckboxes = rowCheckboxes.length;
    
    // If all checkboxes are selected, mark the header as checked
    if (checkedCheckboxes.length === totalCheckboxes ||checkedCheckboxes.length > 0) {
      headerCheckbox.checked = true;
      headerCheckbox.parentElement.classList.add('checked'); // Ensure styling matches checked state
    }
    // If no checkboxes are selected, uncheck the header
    else if (checkedCheckboxes.length === 0) {
      headerCheckbox.checked = false;
      headerCheckbox.parentElement.classList.remove('checked'); // Remove checked styling
    }
  }
  
  // Add an event listener to the header checkbox to select/deselect all checkboxes
  headerCheckbox.addEventListener('change', function () {
    rowCheckboxes.forEach(function (checkbox) {
      checkbox.checked = headerCheckbox.checked;
    });
    // Update the header checkbox state after selecting/deselecting all
    updateHeaderCheckboxState();
  });
  
  // Add event listeners to each row checkbox to update the header checkbox when changed
  rowCheckboxes.forEach(function (checkbox) {
    checkbox.addEventListener('change', function () {
      // Update the header checkbox state based on the row checkboxes
      updateHeaderCheckboxState();
    });
  });
  
  
  // Initial check when the page loads
  updateHeaderCheckboxState();
});
  
function getDropdownValue(event){
  const table = document.querySelector('table');  // Select the table (can be more specific if needed)
  if (event.target && event.target.classList.contains('dropdown-item')) {
      if (event.target.textContent.trim() === 'Wrap Text') {
          table.classList.remove('text-nowrap');  // Remove 'text-nowrap' to allow wrapping
      } else if (event.target.textContent.trim() === 'No Wrap') {
          table.classList.add('text-nowrap');  // Add 'text-nowrap' to disable wrapping
      }
  }
}

// sorting functionality

document.addEventListener("DOMContentLoaded", function () {
  var table = document.getElementById("myTable");
  const theads = table.getElementsByTagName("thead");
  var headers = Array.from(table.querySelectorAll("th")).slice(2); // Skip the first two columns (checkbox and action)
  var clickCounters = Array(headers.length).fill(0); // To track the number of clicks on each column
  
  var originalRows = Array.from(table.rows).slice(theads.length); // Store the original order of rows
  
  // Hide all arrows and reset to default state
  table.querySelectorAll("th i.fa-solid").forEach(icon => {
      icon.style.display = 'none';
      icon.classList.replace("fa-arrow-down", "fa-arrow-up");
  });

  headers.forEach((header, index) => {
      header.addEventListener("click", function () {
          sortTable(index + 2, index); // Pass the column index and header index
      });
  });

  function sortTable(columnIndex, headerIndex) {
      var rows = Array.from(table.rows).slice(theads.length); // Get all rows except the header
      var header = table.querySelector(`th:nth-child(${columnIndex + 1})`);
      var arrow = header.querySelector('i.fa-solid');
      var ascending = arrow.classList.contains("fa-arrow-up");

      // Track number of clicks for the column
      clickCounters[headerIndex]++;

      if (clickCounters[headerIndex] === 3) {
          // Reset to original state (no sorting)
          resetTable();
          clickCounters[headerIndex] = 0; // Reset the counter
          return;
      }

      // Sort rows if it's not the reset state
      rows.sort((row1, row2) => {
          let val1 = getCellValue(row1.cells[columnIndex]);
          let val2 = getCellValue(row2.cells[columnIndex]);
          return (val1 < val2 ? -1 : val1 > val2 ? 1 : 0) * (ascending ? 1 : -1);
      });

      // Reattach sorted rows
      rows.forEach(row => table.querySelector('tbody').appendChild(row));

      // Hide all arrows and reset to default state
      table.querySelectorAll("th i.fa-solid").forEach(icon => {
          icon.style.display = 'none';
          icon.classList.replace("fa-arrow-down", "fa-arrow-up");
      });

      // Show and update the active arrow
      arrow.style.display = 'inline-block';
      arrow.classList.toggle("fa-arrow-up", !ascending);
      arrow.classList.toggle("fa-arrow-down", ascending);
  }

  function resetTable() {
      // Reset to the original rows
      originalRows.forEach(row => table.querySelector('tbody').appendChild(row));

      // Hide all arrows
      table.querySelectorAll("th i.fa-solid").forEach(icon => {
          icon.style.display = 'none';
      });
  }

  function getCellValue(cell) {
      let text = cell.textContent.trim();
      if (!text) return 0;
      if (!isNaN(text.replace(/,/g, ''))) return parseFloat(text.replace(/,/g, '')); // Numbers
      if (text.toLowerCase() === "yes") return 1; // Boolean "Yes/No"
      if (text.toLowerCase() === "no") return 0;
      let date = Date.parse(text);
      return isNaN(date) ? text.toLowerCase() : date; // Date or text
  }
});