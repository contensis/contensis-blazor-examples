﻿using BlazorWebAssemblyApp.Shared;
using Zengenti.Data;

namespace BlazorWebAssemblyApp.Client.Services;

public interface IManagementService
{

    Task<UiProject[]> ListProjects();

    Task<UiProject> GetProject(string projectApiId);

    Task<UiContentType[]> ListContentTypes(string projectApiId);

    Task<UiEntry[]> ListEntries(string projectApiId,
        string contentTypeId = null,
        string language = null,
        PageOptions pageOptions = null,
        IList<string> order = null);

    Task<UiEntry> GetEntry(
        string projectApiId,
        string entryId,
        string language = null,
        string version = null);

    Task<UiEntry> UpdateEntry(
        string projectApiId,
        UiEntry entry,
        string language = null,
        string version = null);

    Task<UiContentType> GetContentTypeFor(string projectApiId, string contentTypeApiId);
}
