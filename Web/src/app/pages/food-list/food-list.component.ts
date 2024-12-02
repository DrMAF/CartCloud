import { Component, OnInit } from '@angular/core';
import { FoodService } from '../services/services';
import { NgFor } from '@angular/common';

export interface FoodItem {
  name: string;
  fat: number;
  carbohydrate: number;
  protein: number;
}

@Component({
  selector: 'app-food-list',
  standalone: true,
  imports: [NgFor],
  templateUrl: './food-list.component.html',
  styleUrls: ['./food-list.component.css']
})

export class FoodListComponent implements OnInit {
  foodList: any[] = [];

  constructor(private foodService: FoodService) { }

  ngOnInit() {
    this.foodList = this.foodService.getTodayList();
  }

  addToTodayList(foodItem: any) {
    console.log(foodItem)
    this.foodService.addToTodayList(foodItem);
  }
}
