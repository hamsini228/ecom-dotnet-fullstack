import { Component, computed, effect, signal } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { Employee } from './models/employee';



//signal() stores reactive state when updated that object immediatly ui changes
//computed() creating derived values automatically from signals
//update() updating signal state based on previous value
//effect() runs side effects whenever signals chnage

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  imports:[DecimalPipe],
  styleUrl: './app.css'
})
export class App {
  employees = signal<Employee[]>([
    { id: 1, name: 'Mahesh', department: 'IT', salary: 50000, isActive: true },
    { id: 2, name: 'Anjali', department: 'HR', salary: 40000, isActive: false },
    { id: 3, name: 'Ravi', department: 'IT', salary: 70000, isActive: true }
  ]);

  totalPayroll = computed(() =>
    this.employees().reduce((sum, emp) => sum + emp.salary, 0)
  );

  activeEmployees = computed(() =>
    this.employees().filter(emp => emp.isActive)
  );

  itEmployees = computed(() =>
    this.employees().filter(emp => emp.department === 'IT')
  );

  constructor() {
    effect(() => {
      console.log('Employee data changed:', this.employees());
      localStorage.setItem('employees', JSON.stringify(this.employees()));
    });
  }
  nextEmployeeId = signal(4);

  addEmployee() {
    const newEmp: Employee = {
      id: this.nextEmployeeId(),
      name: 'New Employee',
      department: 'IT',
      salary: 35000,
      isActive: true
    };

    this.employees.update(empList => [...empList, newEmp]);
    this.nextEmployeeId.update(id => id + 1);
  }

  increaseSalary(id: number) {
    this.employees.update(empList =>
      empList.map(emp =>
        emp.id === id ? { ...emp, salary: emp.salary + 5000 } : emp
      )
    );
  }

  toggleStatus(id: number) {
    this.employees.update(empList =>
      empList.map(emp =>
        emp.id === id ? { ...emp, isActive: !emp.isActive } : emp
      )
    );
  }


}
