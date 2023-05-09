﻿using Microsoft.EntityFrameworkCore;
using Portfolio.DB;
using Portfolio.DB.Models;
using Portfolio.Repository.PortfolioView.Model;
using Portfolio.Repository.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Repository.PortfolioView
{
    public interface IPortfolioViewRepository
    {
        Result Create(string description, string name, bool hasGHLink, string ghLink, List<PortfolioPictureList> pics);
        PortfolioProject GetPortfolioProject(string sid);
        List<PortfolioProject> GetPortfolioProjectList();
    }
    public class PortfolioViewRepository : IPortfolioViewRepository
    {
        private readonly PortfolioDbContext _db;
        public PortfolioViewRepository(PortfolioDbContext db) 
        {
            _db= db;
        }

        public Result Create(string description, string name, bool hasGHLink, string ghLink, List<PortfolioPictureList> pics)
        {
            var listOfPictures = new List<PortfolioPicture>();
            var newPortfolioView = new PortfolioProject
            {
                Description = description,
                Name = name,
                Pictures = listOfPictures,
                HasGHLink = hasGHLink,
                IsActive = true,
                GHLink = ghLink,
                Sid = Guid.NewGuid().ToString(),
            };

            foreach (var p in pics)
            {
                var newP = new PortfolioPicture()
                {
                    Sid = Guid.NewGuid().ToString(),
                    Data = p.File,
                    ProjectId = newPortfolioView.Sid,
                    IsMainPicture = p.IsMainPicture
                };

                newPortfolioView.Pictures.Add(newP);
            }

            try
            {
                _db.Projects.Add(newPortfolioView);
                _db.SaveChanges();
                return new Result() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"Failed to add new View: {ex.Message}" };
            }
        }

        public PortfolioProject GetPortfolioProject(string sid)
        {
            try
            {
                var project = _db.Projects.Include(x => x.Pictures).FirstOrDefault(p => p.Sid == sid);
                if (project == null)
                {
                    return null;
                }
                return project;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PortfolioProject> GetPortfolioProjectList()
        {
            try
            {
                var projectList = _db.Projects.Where(x => x.IsActive).ToList();
                var mainPictures = _db.PortfolioPictures.Where(x => x.IsMainPicture);

                var result = BuildPortfolioProjectObj(projectList, mainPictures);

                if (projectList.Count <= 0)
                {
                    return null;
                }
                return projectList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private List<PortfolioProject> BuildPortfolioProjectObj(List<PortfolioProject> projectList, IQueryable<PortfolioPicture> portfolioPictures)
        {
            foreach(var project in projectList)
            {
                var mainPciture = new List<PortfolioPicture>();
                mainPciture.Add(portfolioPictures.FirstOrDefault(x => x.ProjectId == project.Sid));
                project.Pictures = mainPciture;
            }

            return projectList;
        }
    }
}
